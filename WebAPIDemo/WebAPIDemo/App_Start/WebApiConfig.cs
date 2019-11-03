using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebAPIDemo
{
    public class CustomJsonFormatter : JsonMediaTypeFormatter
    {
        public CustomJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Adding the media type formatters
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // remove the formatter, remove the XML formatters
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            // remove the formatter, remove the JSON formatters
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            // Approach 1. Returning the JSON instead of XML from ASP.Net WebAPI Service when a request is made from the browser
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            // Approach 2. 
            config.Formatters.Add(new CustomJsonFormatter());
        }
    }
}
