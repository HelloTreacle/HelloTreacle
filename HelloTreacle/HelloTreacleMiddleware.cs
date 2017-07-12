using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloTreacle.Policies;
using HelloTreacle.Requests.Store;
using Microsoft.Owin;

namespace HelloTreacle
{
    public class HelloTreacleMiddleware : OwinMiddleware
    {
        private readonly RequestStore requestStore;
        private readonly IEnumerable<Policy> policies;
        private readonly IEnumerable<IEnumerable<string>> propertyKeysUsedInAcrossPolicies;

        public HelloTreacleMiddleware(OwinMiddleware next, RequestStore requestStore, params Policy[] policies)
            : base(next)
        {
            //todo: guard against requestStore and policies being null

            this.requestStore = requestStore;
            this.policies = policies;

            this.propertyKeysUsedInAcrossPolicies = policies.Select(x => x.RequestProperties).Distinct();
        }

        public override async Task Invoke(IOwinContext context)
        {
            Console.WriteLine("Begin Request");

            //run all policies in order
            var policiesStack = new Stack<Policy>(policies);

            while (policiesStack.Count > 0)
            {
                var requestPolicy = policiesStack.Pop();

                var policyViolation = await requestPolicy.Run(requestStore, context);

                if (policyViolation)
                    return;
            }

            //carry on
            await Next.Invoke(context);

            //store the request
            await requestStore.Store(context.Request);

            Console.WriteLine("End Request");
        }
    }
}
