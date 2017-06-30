﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloTreacle.Policies;
using HelloTreacle.Requests.Store;
using Microsoft.Owin;

namespace HelloTreacle
{
    public class HelloTreacleMiddleware : OwinMiddleware
    {
        private readonly RequestStore requestStore;
        private readonly IEnumerable<RequestPolicy> policies;

        public HelloTreacleMiddleware(OwinMiddleware next, RequestStore requestStore, IEnumerable<RequestPolicy> policies)
            : base(next)
        {
            this.requestStore = requestStore;
            this.policies = policies;
        }

        public override async Task Invoke(IOwinContext context)
        {
            Console.WriteLine("Begin Request");

            //run all policies in order
            var policiesStack = new Stack<RequestPolicy>(policies);

            while (policiesStack.Count > 0)
            {
                var requestPolicy = policiesStack.Pop();

                var policyViolation = await requestPolicy.Run(requestStore, context);

                if (policyViolation)
                    return;
            }

            //store the request
            await requestStore.Store(context.Request);

            //carry on
            await Next.Invoke(context);

            Console.WriteLine("End Request");
        }
    }
}
