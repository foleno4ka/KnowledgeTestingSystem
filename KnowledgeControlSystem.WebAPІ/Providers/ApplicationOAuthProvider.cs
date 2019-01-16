using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using KnowledgeControlSystem.BLL.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;

namespace KnowledgeControlSystem.WebAPІ.Providers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserService _userService;

        public ApplicationOAuthProvider(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _userService.FindUser(context.UserName, context.Password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Name", user.UserName));
                identity.AddClaim(new Claim("Id", user.Id.ToString()));
                var userRoles = _userService.GetRoles(user.Id);
                foreach (string roleName in userRoles)
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                var additionalData =
                    new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {"role", JsonConvert.SerializeObject(userRoles)}
                    });
                var ticket = new AuthenticationTicket(identity, additionalData);
                context.Validated(ticket);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}