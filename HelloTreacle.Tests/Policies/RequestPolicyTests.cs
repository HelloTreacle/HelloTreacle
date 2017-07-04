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
                    var requestPolicy = RequestPolicy
                        .WithPrerequisites(
                            RequestPath.Equals("/test")
                        );

                    Assert.IsFalse(ValidatePath(requestPolicy, "/not_test"));
                    Assert.IsTrue(ValidatePath(requestPolicy, "/test"));
                }

                [Test]
                public void Multiple_RequestPathEquals()
                {
                    var requestPolicy = RequestPolicy
                        .WithPrerequisites(
                            PrerequisiteOperator.Or,
                            RequestPath.Equals("/test"),
                            RequestPath.Equals("/test2")
                        );

                    Assert.IsFalse(ValidatePath(requestPolicy, "/not_test"));
                    Assert.IsTrue(ValidatePath(requestPolicy, "/test"));
                    Assert.IsTrue(ValidatePath(requestPolicy, "/test2"));
                }

                [Test]
                public void Single_RequestPathNotEquals()
                {
                    var requestPolicy = RequestPolicy
                        .WithPrerequisites(
                            RequestPath.NotEquals("/test")
                        );

                    Assert.IsFalse(ValidatePath(requestPolicy, "/test"));
                    Assert.IsTrue(ValidatePath(requestPolicy, "/not_test"));
                }

                private bool ValidatePath(RequestPolicy policy, string path)
                {
                    var owinRequest = new OwinRequest { Path = new PathString(path) };

                    var valids = policy.Prerequisites.Select(p => p.Invoke(owinRequest));

                    return policy.PrerequisitesOperator == PrerequisiteOperator.Or 
                        ? valids.Any(x => x.Equals(true)) 
                        : valids.All(x => x.Equals(true));
                }
            }
        }
    }
}
