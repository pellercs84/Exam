using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Web.Http;

namespace Exam.WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.Clear();
            config.Routes.MapHttpRoute(
                name: "Default API1",
                routeTemplate: "api/{controller}/{id}/{manufacturerId}",
                defaults: new { id = RouteParameter.Optional, manufacturerId = RouteParameter.Optional, controller = "products" }
                );
            config.Routes.MapHttpRoute(
                name: "Default API",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            
        }
            
        
    }
}
