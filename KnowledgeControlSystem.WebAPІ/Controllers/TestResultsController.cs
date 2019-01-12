using System.Collections.Generic;
using System.Linq;
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
    public class TestResultsController : ApiController
    {
        private readonly ITestResultService _testResultService;

        public TestResultsController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
        }

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