using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace al.performancemanagement.App
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

          
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSystemDiagnosticsTracing();
            config.Services.Replace(typeof(ITraceWriter), new WebApiTracer());
            config.MessageHandlers.Add(new CorsHandler());
            config.MessageHandlers.Add(new WebApiDelegatingHandler());
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
