using KnowledgeControlSystem.WebAPІ;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup("WebApiStart", typeof(WebApiStartup))]

namespace KnowledgeControlSystem.WebAPІ
{
    public partial class WebApiStartup
    {
        // IUserService _userService { get; set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            IocConfig.Configure();
            ConfigureAuth(app);
        }
    }
}