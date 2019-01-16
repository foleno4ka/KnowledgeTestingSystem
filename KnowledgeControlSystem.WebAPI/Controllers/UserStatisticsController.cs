using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.WebAPI.Infrastructure;

namespace KnowledgeControlSystem.WebAPI.Controllers
{
    [Authorize]
    public class TestStatisticsController : ApiController
    {
        private readonly IStatisticService _statisticService;

        public TestStatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        /// <summary>
        /// Returns test statistics of current user
        /// </summary>
        /// <returns></returns>
        [Route("api/TestStatistics")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetTestStatistics()
        {
            int userId = ControllerHelper.GetCurrentUserId(User);
            TestStatisticDTO testStatistics = _statisticService.GetStatistics(userId);
            return Request.CreateResponse(HttpStatusCode.OK, testStatistics);
        }
        /// <summary>
        /// Returns all users statistics for admin/moderator
        /// </summary>
        /// <returns></returns>
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