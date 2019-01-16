using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.Common;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.WebAPІ.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = KnowledgeRoles.Admin)]
        public HttpResponseMessage GetAllUsers()
        {
            IEnumerable<UserDTO> users = _userService.GetAll().ToList();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        /// <summary>
        /// Creates new user with default role "user"
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(UserDTO model)
        {
            var result = _userService.Create(model);
            UserDTO user = _userService.GetByName(model.UserName);
            model.Roles = new[] {KnowledgeRoles.User};
            _userService.AddToUserRoles(user.Id, model.Roles);
            return result;
        }

        /// <summary>
        /// Deletes user 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{userId}")]
        [Authorize(Roles = KnowledgeRoles.Admin)]
        public HttpResponseMessage DeleteUser(int userId)
        {
            UserDTO user = _userService.Get(userId);
            if (user == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Such User does not exist");
            _userService.Delete(user);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

    }
}