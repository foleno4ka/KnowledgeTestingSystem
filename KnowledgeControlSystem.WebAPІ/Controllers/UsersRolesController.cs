using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.Common;

namespace KnowledgeControlSystem.WebAPІ.Controllers
{
    [Authorize(Roles = KnowledgeRoles.Admin)]
    [RoutePrefix("api/Users/{userId}/Roles")]
    public class UsersRolesController : ApiController
    {
        private readonly IUserService _userService;

        public UsersRolesController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetUserRoles(int userId)
        {
            IEnumerable<string> userRoles = _userService.GetRoles(userId);
            if (userRoles == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Roles not found");
            return Request.CreateResponse(HttpStatusCode.OK, userRoles);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage UpdateUserRoles(int userId, string[] roles)
        {
            _userService.AddToUserRoles(userId, roles);
            return Request.CreateResponse(HttpStatusCode.OK, "User roles updated");
        }

        [HttpPut]
        [Route("{roleName}")]
        public HttpResponseMessage AddToRole(int userId, string roleName)
        {
            _userService.AddToUserRoles(userId, roleName);
            return Request.CreateResponse(HttpStatusCode.NoContent, $"added role {roleName} to {userId}");
        }

        [HttpDelete]
        [Route("{roleName}")]
        public HttpResponseMessage DeleteFromRole(int userId, string roleName)
        {
            _userService.DeleteFromRole(userId, roleName);
            return Request.CreateResponse(HttpStatusCode.OK, $"deleted role {roleName} of {userId}");
        }
    }
}