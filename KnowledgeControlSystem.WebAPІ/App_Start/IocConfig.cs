using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using KnowledgeControlSystem.BLL.Autofac;
using KnowledgeControlSystem.WebAPІ.Providers;
using Microsoft.Owin.Security.OAuth;

namespace KnowledgeControlSystem.WebAPІ
{
    public static class IocConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new BllConfigModule() {Connection = "name=KnowledgeDBContext"});
            builder.RegisterType<ApplicationOAuthProvider>().As<IOAuthAuthorizationServerProvider>()
                .PropertiesAutowired();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}