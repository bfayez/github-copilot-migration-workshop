using System.Web;
using System.Web.Http;

namespace MessageService
{
    /// <summary>
    /// Global application class for the MessageService Web API
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Application start event handler
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
