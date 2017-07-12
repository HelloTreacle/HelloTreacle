using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using HelloTreacle.Policies;
using HelloTreacle.Requests.Store;
using Microsoft.Owin;
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
                new Policy(
                    TimeSpan.FromSeconds(10),
                    5,
                    new[] { "LocalIpAddress" },
                    ctx =>
                    {
                        ctx.Response.Redirect("http://www.google.com");
                    })
            };


            appBuilder.UseHelloTreacle(requestPolicies);

            //set as unauth
            appBuilder.Use(async (context, next) =>
            {
                if (context.Request.QueryString.Value.Contains("unauth"))
                    context.Response.StatusCode = 401;
            });

            appBuilder.UseWebApi(config);
        }
    }
}