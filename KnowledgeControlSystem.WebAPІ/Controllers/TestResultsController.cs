using System;
using System.Collections.Generic;
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
    public class TestResultsController : ApiController
    {
        IService<TestResultDTO> _testResultService;
        IUserService _userService;
        IService<TestDTO> _testService;
        public TestResultsController(IService<TestResultDTO> testResultService, IUserService userService, IService<TestDTO> testService)
        {
            _testResultService = testResultService;
            _userService = userService;
            _testService = testService;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetTestResults(int userId)
        {
            if (_userService.Get(userId) == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
            IEnumerable<TestResultDTO> testResult = _testResultService.GetAll().Where(test => test.UserId == userId);
            if (testResult == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "TestResults not found");
            return this.Request.CreateResponse(HttpStatusCode.OK, testResult);
        }
        
        
        public HttpResponseMessage GetAllTestResult()
        {
            return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Test Results not found");

        }
    }
}
