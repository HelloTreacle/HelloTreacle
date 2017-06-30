using System;
using System.Collections.Generic;
using System.Web.Http;
using HelloTreacle.Policies;
using HelloTreacle.Requests.Store;
using Owin;

namespace HelloTreacle.WebApiHost
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var requestPolicies = new[]
            {
                new RequestPolicy(
                    TimeSpan.FromSeconds(10),
                    5,
                    new[] { "LocalIpAddress" },
                    ctx =>
                    {
                        ctx.Response.Redirect("http://www.google.com");
                    })
            };

            appBuilder.UseHelloTreacle(requestPolicies);

            appBuilder.UseWebApi(config);
        }
    }
}