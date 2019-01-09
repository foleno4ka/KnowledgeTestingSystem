using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;

namespace KnowledgeControlSystem.WebAPІ.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    public class RolesController : ApiController
    {
        private readonly IService<RoleDTO> _roleService;
        public RolesController(IService<RoleDTO> roleService)
        {
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/GetAllRoles")]
        public HttpResponseMessage GetRoles()
        {
            var roles = _roleService.GetAll()
                .Select(x => new { x.Id, x.Name })
                .ToList();
            return this.Request.CreateResponse(HttpStatusCode.OK, roles);
        }
    }
}
