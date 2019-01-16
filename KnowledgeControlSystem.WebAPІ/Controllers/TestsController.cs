using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.Common;
using KnowledgeControlSystem.WebAPІ.Infrastructure;

namespace KnowledgeControlSystem.WebAPІ.Controllers
{
    [Authorize]
    [RoutePrefix("api/Tests")]
    public class TestsController : ApiController
    {
        private readonly ITestService _testService;

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }
        
        /// <summary>
        /// Returns all records
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            IEnumerable<TestDTO> tests = _testService.GetAll();
            if (tests == null)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Tests not found");
            return Request.CreateResponse(HttpStatusCode.OK, tests);
        }

        [Route("")]
        [Authorize(Roles = KnowledgeRoles.Admin + "," + KnowledgeRoles.Moderator)]
        [HttpPost]
        public HttpResponseMessage PostTest([FromBody] TestDTO addedTest)
        {
            _testService.Create(addedTest);
            return Request.CreateResponse(HttpStatusCode.OK, "Product added");
        }

        [Route("{testId}")]
        [HttpGet]
        public HttpResponseMessage GetTest(int testId)
        {
            TestDTO test = _testService.Get(testId);
            if (test == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Test not found");
            return Request.CreateResponse(HttpStatusCode.OK, test);
        }

        [Route("{testId}")]
        [Authorize(Roles = KnowledgeRoles.Admin + "," + KnowledgeRoles.Moderator)]
        [HttpPut]
        public HttpResponseMessage PutTest(int testId, TestDTO changedTest)
        {
            if (changedTest == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");
            _testService.Update(changedTest);
            return Request.CreateResponse(HttpStatusCode.NoContent, "Test updated");
        }

        [Route("{testId}")]
        [Authorize(Roles = KnowledgeRoles.Admin + "," + KnowledgeRoles.Moderator)]
        [HttpDelete]
        public HttpResponseMessage DeleteTest(int testId)
        {
            _testService.Delete(testId);
            return Request.CreateResponse(HttpStatusCode.NoContent, "Test deleted");
        }
        
        /// <summary>
        /// Starts test
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{testId}/Start")]
        public HttpResponseMessage StartTest(int testId)
        {
            int userId = ControllerHelper.GetCurrentUserId(User);
            DateTime testStartTime = _testService.StartTest(testId, userId);
            return Request.CreateResponse(HttpStatusCode.OK, testStartTime);
        }

        /// <summary>
        /// Finishes test and updates test result
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="userAnswers"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{testId}/Finish")]
        public HttpResponseMessage FinishTest(int testId, Dictionary<int, int[]> userAnswers)
        {
            int userId = ControllerHelper.GetCurrentUserId(User);
            TestResultDTO testResult = _testService.FinishTest(testId, userId, userAnswers);
            return Request.CreateResponse(HttpStatusCode.OK, testResult);
        }
    }
}