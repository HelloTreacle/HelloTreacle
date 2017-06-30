using System.Collections.Generic;
using HelloTreacle.Policies;
using HelloTreacle.Requests.Store;
using Owin;

namespace HelloTreacle
{
    public static class HelloTreacleMiddlewareHandler
    {
        public static IAppBuilder UseHelloTreacle(this IAppBuilder app, RequestStore requestStore, IEnumerable<RequestPolicy> requestPolicies)
        {
            app.Use<HelloTreacleMiddleware>(requestStore, requestPolicies);
            return app;
        }
    }
}