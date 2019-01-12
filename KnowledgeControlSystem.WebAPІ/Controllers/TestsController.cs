using System;
using System.Collections.Generic;
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
    //[AllowAnonymous]
    [Authorize]
    public class TestsController : ApiController
    {
        private readonly ITestService _testService;

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        [Route("api/Tests/{id}")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetTest(int id)
        {
            TestDTO test = _testService.Get(id);
            if (test == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Test not found");
            return Request.CreateResponse(HttpStatusCode.OK, test);
        }

        [Route("api/Tests")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            IEnumerable<TestDTO> tests = _testService.GetAll();
            if (tests == null)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Tests not found");
            return Request.CreateResponse(HttpStatusCode.OK, tests);
        }

        [Route("api/Tests/{id:int}")]
        //[Authorise(Roles="Admin")]
        [HttpPut]
        public HttpResponseMessage PutTest(int id, TestDTO changedTest)
        {
            if (changedTest == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");
            _testService.Update(changedTest);
            return Request.CreateResponse(HttpStatusCode.NoContent, "Test updated");
        }

        [Route("api/Tests")]
        //[Authorise(Roles="Admin")]
        [HttpPost]
        public HttpResponseMessage PostTest([FromBody] TestDTO addedTest)
        {
            _testService.Create(addedTest);
            return Request.CreateResponse(HttpStatusCode.OK, "Product added");
        }

        [Route("api/Tests/{id}")]
        //[Authorize(Roles="Admin")]
        [HttpDelete]
        public HttpResponseMessage DeleteTest(int id)
        {
            _testService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent, "Test deleted");
        }

        [HttpPost]
        [Authorize]
        [Route("api/Tests/{testId}/Start")]
        public HttpResponseMessage StartTest(int testId)
        {
            int userId = ControllerHelper.GetCurrentUserId(User);
            DateTime testStartTime = _testService.StartTest(testId, userId);
            return Request.CreateResponse(HttpStatusCode.OK, testStartTime);
        }

        [HttpPost]
        [Authorize]
        [Route("api/Tests/{testId}/Finish")]
        public HttpResponseMessage FinishTest(int testId, Dictionary<int, int[]> userAnswers)
        {
            int userId = ControllerHelper.GetCurrentUserId(User);
            TestResultDTO testResult = _testService.FinishTest(testId, userId, userAnswers);
            return Request.CreateResponse(HttpStatusCode.OK, testResult);
        }
    }
}