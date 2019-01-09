using System;
using System.Web.Http;
using KnowledgeControlSystem.WebAPІ;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup("WebApiStart", typeof(WebApiStartup))]

namespace KnowledgeControlSystem.WebAPІ
{
    public partial class WebApiStartup
    {
        public static string PublicClientId { get; private set; }

        public void ConfigurationAuth(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            PublicClientId = "self";
            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider =
                    (IOAuthAuthorizationServerProvider) GlobalConfiguration.Configuration.DependencyResolver.GetService(
                        typeof(IOAuthAuthorizationServerProvider)),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}