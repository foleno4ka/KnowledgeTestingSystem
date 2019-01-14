using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StatisticService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TestResultEntity, TestResultDTO>()
                    .ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.Test.Name))
                    .ForMember(dest => dest.MaxDuration, opt => opt.MapFrom(src => src.Test.Duration));
                cfg.CreateMap<TestResultDTO, TestResultEntity>();
            }).CreateMapper();
        }

        public TestStatisticDTO GetStatistics(int userId)
        {
            List<TestResultEntity> testResults = _unitOfWork.TestResults
                .FindBy((e) => e.UserId == userId && e.EndTime != DateTime.MinValue).ToList();
            TestStatisticDTO testStatistics = new TestStatisticDTO();
            if (testResults.Any())
            {
                testStatistics.PassedTestCount = testResults.Count();
                testStatistics.AvgScorePercent = Math.Round(GetAvgScorePercent(testResults));
                testStatistics.AvgTimeSeconds = Math.Round(GetAvgTime(testResults));
            }

            return testStatistics;
        }

        private double GetAvgScorePercent(List<TestResultEntity> testResults)
        {
            return testResults.ToList().Average(testResult =>
                (testResult.Score / testResult.TotalScore) * 100);
        }

        private double GetAvgTime(IEnumerable<TestResultEntity> testResults)
        {
            return testResults.Average(testresult => (testresult.EndTime - testresult.StartTime).TotalSeconds);
        }

        public IEnumerable<TestStatisticDTO> GetAllUserStatistics()
        {
            IEnumerable<IdentityUserEntity> userList = _unitOfWork.Users.GetAllUsers();
            List<TestStatisticDTO> testStatistics = new List<TestStatisticDTO>();
            userList.ToList().ForEach(user =>
            {
                TestStatisticDTO testStatistic = GetStatistics(user.Id);
                testStatistic.UserName = user.UserName;
                testStatistics.Add(testStatistic);
            });

            return testStatistics;
        }
    }
}