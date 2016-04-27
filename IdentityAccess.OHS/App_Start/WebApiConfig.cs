using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace IdentityAccess.OHS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
       
            // Web API 路由
            config.MapHttpAttributeRoutes();
           // config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
