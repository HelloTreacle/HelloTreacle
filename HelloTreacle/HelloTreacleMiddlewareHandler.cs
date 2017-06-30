using System.Collections.Generic;
using HelloTreacle.Policies;
using HelloTreacle.Requests.Store;
using Owin;

namespace HelloTreacle
{
    public static class HelloTreacleMiddlewareHandler
    {
        public static IAppBuilder UseHelloTreacle(this IAppBuilder app, IEnumerable<RequestPolicy> requestPolicies, RequestStore requestStore = null)
        {
            if (requestStore == null)
                requestStore = new InMemoryRequestStore();

            app.Use<HelloTreacleMiddleware>(requestStore, requestPolicies);

            return app;
        }
    }
}