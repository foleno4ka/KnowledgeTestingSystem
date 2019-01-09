using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.WebAPІ.Infrastructure;

namespace KnowledgeControlSystem.WebAPІ.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class TestStatisticsController : ApiController
    {
        private readonly IStatisticService _statisticService;

        public TestStatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [Route("api/TestStatistics")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetTestStatistics()
        {
            int userId = ControllerHelper.GetCurrentUserId(User);
            TestStatisticDTO testStatistics = _statisticService.GetStatistics(userId);
            return Request.CreateResponse(HttpStatusCode.OK, testStatistics);
        }
    }
}