using System;
using System.Linq;
using HelloTreacle.Policies;
using Microsoft.Owin;
using NUnit.Framework;

namespace HelloTreacle.Tests.Policies
{
    [TestFixture]
    public class RequestPolicyTests
    {
        public class FluentAPI : RequestPolicyTests
        {
            //RequestPolicy
            //	WithPrerequisites
            //		Verbs
            //		Paths
            //		Authentication
            //		Custom
            //	WithMatchingRequestParameters
            //	WithFrequencyThreshold
            //	OnViolation(ctx => )

            public class WithPrerequisites : FluentAPI
            {
                [Test]
                public void Single_RequestPathEquals()
                {
                    var result = RequestPolicy
                        .WithPrerequisites(
                            RequestPath.Equals("/test")
                        );

                    var requestPolicyPrerequisites = result.Prerequisites;

                    Assert.That(requestPolicyPrerequisites, Is.Not.Null);

                    var requestPolicyPrerequisite = requestPolicyPrerequisites.FirstOrDefault();

                    Func<IOwinRequest, bool> checker = i => i.Path.HasValue && i.Path.Value == "/test";

                    //rubbish assertion!
                    Assert.AreEqual(checker.ToString(), requestPolicyPrerequisite.ToString());
                }

                [Test]
                public void Multiple_RequestPathEquals()
                {
                    var result = RequestPolicy
                        .WithPrerequisites(
                            RequestPath.Equals("/test"),
                            RequestPath.Equals("/test2")
                        );

                    var requestPolicyPrerequisites = result.Prerequisites;

                    Assert.That(requestPolicyPrerequisites, Is.Not.Null);

                    var requestPolicyPrerequisite = requestPolicyPrerequisites.FirstOrDefault();

                    Func<IOwinRequest, bool> checker = i => i.Path.HasValue && i.Path.Value == "/test";

                    Assert.Fail("Not yet implemented");
                }

                [Test]
                public void Single_RequestPathNotEquals()
                {
                    var result = RequestPolicy
                        .WithPrerequisites(
                            RequestPath.NotEquals("/test")
                        );

                    var requestPolicyPrerequisites = result.Prerequisites;

                    Assert.That(requestPolicyPrerequisites, Is.Not.Null);

                    var requestPolicyPrerequisite = requestPolicyPrerequisites.FirstOrDefault();

                    Func<IOwinRequest, bool> checker = i => i.Path.HasValue && i.Path.Value == "/test";

                    Assert.Fail("Not yet implemented");
                }
            }
        }
    }
}
