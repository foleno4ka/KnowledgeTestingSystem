using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.Common;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.WebAPІ.Controllers
{
    [AllowAnonymous]
    public class AccountsController : ApiController
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(UserDTO model)
        {
            var result = _userService.Create(model);
            UserDTO user = _userService.GetByName(model.UserName);
            model.Roles = new[]{ KnowledgeRoles.User};
            _userService.AddToUserRoles(user.Id, model.Roles);
            return result;
        }

        [Route("api/CurrentUser")]
        [HttpGet]
        [Authorize]
        public async Task<UserDTO> GetCurrentUser()
        {
            //var wwww = User.Identity.GetUserName();
            //int userId = Convert.ToInt32(HttpContext.Current.User.Identity.GetUserId());
            //return await _userService.Get(userId);
            var identityClaims = (ClaimsIdentity) User.Identity;
            //IEnumerable<Claim> claims = identityClaims.Claims;
            UserDTO model = new UserDTO();
            model.UserName = identityClaims.FindFirst("Name").Value;
            model.Id = Convert.ToInt32(identityClaims.FindFirst("Id").Value);
            return model;
        }
    }
}