using System.Web.Http;
using System.Web.Http.Cors;

namespace MessageService
{
    /// <summary>
    /// Web API configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers Web API configuration
        /// </summary>
        /// <param name="config">HTTP configuration</param>
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS to allow the console application to call the API
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
