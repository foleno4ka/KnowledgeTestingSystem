using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.WebAPІ.Infrastructure;

namespace KnowledgeControlSystem.WebAPІ.Controllers
{
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

        [Route("api/AllTestStatistics")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetAllTestStatistics()
        {
            IEnumerable<TestStatisticDTO> testStatistics = _statisticService.GetAllUserStatistics();
            return Request.CreateResponse(HttpStatusCode.OK, testStatistics);
        }
    }
}