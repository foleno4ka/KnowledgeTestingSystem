using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.Common;

namespace KnowledgeControlSystem.WebAPІ.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize(Roles = KnowledgeRoles.Admin)]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/Users")]
        public HttpResponseMessage GetAllUsers()
        {
            IEnumerable<UserDTO> users = _userService.GetAll().ToList();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [HttpDelete]
        [Route("api/Users/{id}")]
        public HttpResponseMessage DeleteUser(int id)
        {
            UserDTO user = _userService.Get(id);
            if (user == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Such User does not exist");
            _userService.Delete(user);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpGet]
        [Route("api/Users/{userId}/Roles")]
        public HttpResponseMessage GetUserRole(int userId)
        {
            IEnumerable<string> userRoles = _userService.GetRoles(userId);
            if (userRoles == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Roles not found");
            return Request.CreateResponse(HttpStatusCode.OK, userRoles);
        }

        [HttpPost]
        [Route("api/Users/{userId}/Roles")]
        public HttpResponseMessage UpdateUserRoles(int userId, string[] roles)
        {
            _userService.AddToUserRoles(userId, roles);
            return Request.CreateResponse(HttpStatusCode.OK, "User roles updated");
        }

        [HttpPut]
        [Route("api/Users/{userId}/{roleName}")]
        public HttpResponseMessage AddToRole(int userId, string roleName)
        {
            _userService.AddToUserRoles(userId, roleName);
            return Request.CreateResponse(HttpStatusCode.NoContent, $"added role {roleName} to {userId}");
        }

        [HttpDelete]
        [Route("api/Users/{userId}/{roleName}")]
        public HttpResponseMessage DeleteFromRole(int userId, string roleName)
        {
            _userService.DeleteFromRole(userId, roleName);
            return Request.CreateResponse(HttpStatusCode.OK, $"deleted role {roleName} of {userId}");
        }
    }
}