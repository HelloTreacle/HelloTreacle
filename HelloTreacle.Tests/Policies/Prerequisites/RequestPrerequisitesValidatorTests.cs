using System.Collections.Generic;
using System.Linq;
using HelloTreacle.Policies;
using Microsoft.Owin;
using NUnit.Framework;

namespace HelloTreacle.Tests.Policies.Prerequisites
{
    [TestFixture]
    public class RequestPrerequisitesValidatorTests
    {
        protected RequestPrerequisiteValidator Validator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Validator = new RequestPrerequisiteValidator();
        }

        public class Paths : RequestPrerequisitesValidatorTests
        {
            [Test]
            public void Single_RequestPathEquals()
            {
                var requestPolicy = RequestPolicy
                    .WithPrerequisites(
                        RequestPrerequisite.Path.Equals("/test")
                    );

                Assert.IsFalse(Validator.Validate(OwinRequestWithPath("/not_test"), requestPolicy.Prerequisites));
                Assert.IsTrue(Validator.Validate(OwinRequestWithPath("/test"), requestPolicy.Prerequisites));
            }

            [Test]
            public void Multiple_RequestPathEquals()
            {
                var requestPolicy = RequestPolicy
                    .WithPrerequisites(
                        RequestPrerequisite.Path.Equals("/test"),
                        RequestPrerequisite.Path.Equals("/test2")
                    );

                Assert.IsFalse(Validator.Validate(OwinRequestWithPath("/not_test"), requestPolicy.Prerequisites));
                Assert.IsTrue(Validator.Validate(OwinRequestWithPath("/test"), requestPolicy.Prerequisites));
                Assert.IsTrue(Validator.Validate(OwinRequestWithPath("/test2"), requestPolicy.Prerequisites));
            }

            [Test]
            public void Mixed_RequestPathEquals_With_RequestPathNotEquals()
            {
                var requestPolicy = RequestPolicy
                    .WithPrerequisites(
                        RequestPrerequisite.Path.Equals("/test"),
                        RequestPrerequisite.Path.Equals("/test2"),
                        RequestPrerequisite.Path.NotEquals("/test3")
                    );

                Assert.IsTrue(Validator.Validate(OwinRequestWithPath("/test"), requestPolicy.Prerequisites));
                Assert.IsTrue(Validator.Validate(OwinRequestWithPath("/test2"), requestPolicy.Prerequisites));
                Assert.IsFalse(Validator.Validate(OwinRequestWithPath("/test3"), requestPolicy.Prerequisites));
            }

            [Test]
            public void Single_RequestPathContains()
            {
                var requestPolicy = RequestPolicy
                    .WithPrerequisites(
                        RequestPrerequisite.Path.Contains("test")
                    );

                Assert.IsTrue(Validator.Validate(OwinRequestWithPath("/something/containing/test"), requestPolicy.Prerequisites));
                Assert.IsFalse(Validator.Validate(OwinRequestWithPath("/nothing"), requestPolicy.Prerequisites));
            }

            [Test]
            public void Single_RequestPathDoesNotContain()
            {
                var requestPolicy = RequestPolicy
                    .WithPrerequisites(
                        RequestPrerequisite.Path.DoesNotContain("test")
                    );

                Assert.False(Validator.Validate(OwinRequestWithPath("/something/containing/test"), requestPolicy.Prerequisites));
                Assert.IsTrue(Validator.Validate(OwinRequestWithPath("/nothing"), requestPolicy.Prerequisites));
            }

            private static IOwinRequest OwinRequestWithPath(string path)
            {
                return new OwinRequest { Path = new PathString(path) };
            }
        }

        public class RequestPrerequisiteValidator
        {
            public bool Validate(IOwinRequest request, IEnumerable<Prerequisite> prerequisites)
            {
                var requestPrerequisites = prerequisites.Select(x => x.RequestCondition);

                var results = requestPrerequisites.Select(p => p.Invoke(request));

                return results.Any(res => res.Equals(true));
            }
        }
    }
}
