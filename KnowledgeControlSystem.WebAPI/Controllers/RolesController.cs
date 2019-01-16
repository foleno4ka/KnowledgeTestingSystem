using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.Common;

namespace KnowledgeControlSystem.WebAPI.Controllers
{
    public class RolesController : ApiController
    {
        private readonly IService<RoleDTO> _roleService;

        public RolesController(IService<RoleDTO> roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// Returns all roles
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = KnowledgeRoles.Admin)]
        [HttpGet]
        [Route("api/Roles")]
        public HttpResponseMessage GetRoles()
        {
            IEnumerable<RoleDTO> roles = _roleService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, roles);
        }
    }
}