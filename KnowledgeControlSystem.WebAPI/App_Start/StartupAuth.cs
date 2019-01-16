using System;
using System.Web.Http;
using KnowledgeControlSystem.WebAPI;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup("WebApiStart", typeof(WebApiStartup))]

namespace KnowledgeControlSystem.WebAPI
{
    public partial class WebApiStartup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider =
                    (IOAuthAuthorizationServerProvider) GlobalConfiguration.Configuration.DependencyResolver.GetService(
                        typeof(IOAuthAuthorizationServerProvider)),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(4),
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}