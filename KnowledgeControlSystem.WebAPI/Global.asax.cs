using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KnowledgeControlSystem.WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
        }
    }
}