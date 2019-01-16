using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.WebAPI.Infrastructure;

namespace KnowledgeControlSystem.WebAPI.Controllers
{
    public class TestResultsController : ApiController
    {
        private readonly ITestResultService _testResultService;

        public TestResultsController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
        }
        
        /// <summary>
        /// Returns all test results of current user
        /// </summary>
        /// <returns></returns>
        [Route("api/TestResults")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetTestResults()
        {
            int userId = ControllerHelper.GetCurrentUserId(User);
            IEnumerable<TestResultDTO> testResults = _testResultService.FindByUser(userId);
            if (!testResults.Any())
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TestResults not found");
            return Request.CreateResponse(HttpStatusCode.OK, testResults);
        }
    }
}