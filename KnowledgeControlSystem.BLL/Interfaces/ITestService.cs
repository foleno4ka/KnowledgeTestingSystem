using System;
using System.Collections.Generic;
using KnowledgeControlSystem.BLL.DTOs;

namespace KnowledgeControlSystem.BLL.Interfaces
{
    public interface ITestService:IService<TestDTO>
    {
        DateTime StartTest(int testId, int userId);
        TestResultDTO FinishTest(int testId, int userId, Dictionary<int, int[]> userAnswersDict);
    }
}