using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TestStatisticDTO GetStatistics(int userId)
        {
            List<TestResultEntity> testResults = _unitOfWork.TestResults
                .FindBy((e) => e.UserId == userId && e.EndTime != DateTime.MinValue).ToList();
            TestStatisticDTO testStatistics = new TestStatisticDTO
            {
                PassedTestCount = testResults.Count,
                AvgScorePercent = Math.Round(GetAvgScorePercent(testResults)),
                AvgTimeSeconds = Math.Round(GetAvgTime(testResults))
            };

            return testStatistics;
        }

        private double GetAvgScorePercent(IEnumerable<TestResultEntity> testResults)
        {
            return testResults.ToList().Average(testResult =>
                (testResult.Score / testResult.TotalScore) * 100);
        }

        private double GetAvgTime(IEnumerable<TestResultEntity> testResults)
        {
            return testResults.Average(testresult => (testresult.EndTime - testresult.StartTime).TotalSeconds);
        }
    }
}