using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Exceptions;
using KnowledgeControlSystem.BLL.Infrastructure;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        // Another mapper to remove correct answer for non-admin and non-moderators (not implemented)
        IMapper _userMapper;

        public TestService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();

                cfg.CreateMap<TestEntity, TestDTO>()
                    .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
                cfg.CreateMap<QuestionEntity, QuestionDTO>()
                    .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

                cfg.CreateMap<TestDTO, TestEntity>()
                    .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

                cfg.CreateMap<QuestionDTO, QuestionEntity>()
                    .ForMember(dest => dest.Test, opt => opt.Ignore());
                cfg.CreateMap<AnswerDTO, AnswerEntity>().ForMember(dest => dest.Question, opt => opt.Ignore());
            }).CreateMapper();
            _userMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<TestEntity, TestDTO>()
                    .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
                cfg.CreateMap<TestDTO, TestEntity>();
            }).CreateMapper();
        }

        public void Create(TestDTO test)
        {
            TestEntity testEnity = _mapper.Map<TestEntity>(test);
            _unitOfWork.Tests.Create(testEnity);
            _unitOfWork.Save();
        }

        public void Delete(int testId)
        {
            TestEntity test = _unitOfWork.Tests.Get(testId);
            _unitOfWork.Tests.Remove(test);
            _unitOfWork.Save();
        }

        public TestDTO Get(int id)
        {
            TestEntity test = _unitOfWork.Tests.Get(id);
            return _mapper.Map<TestDTO>(test);
        }

        public IEnumerable<TestDTO> GetAll()
        {
            return _unitOfWork.Tests.GetAll().Select(test => _mapper.Map<TestDTO>(test));
        }

        public void Update(TestDTO dto)
        {
            if (_unitOfWork.TestResults.FindBy(entity => entity.TestId == dto.Id && entity.EndTime == DateTime.MinValue).Any())
                throw new TestInUseException();
            //TestEntity test = _unitOfWork.Tests.Get(dto.Id);
            //_unitOfWork.Tests.Update(_mapper.Map<TestDTO, TestEntity>(dto, test));
            _unitOfWork.Tests.Update(_mapper.Map<TestEntity>(dto));
            _unitOfWork.Save();
        }

        public DateTime StartTest(int testId, int userId)
        {
            TestResultEntity testResult = _unitOfWork.TestResults.FindOneBy(entity =>
                entity.TestId == testId && entity.UserId == userId && entity.EndTime == DateTime.MinValue);
            if (testResult == null)
            {
                testResult = new TestResultEntity()
                {
                    TestId = testId,
                    UserId = userId,
                    StartTime = DateTime.Now,
                    EndTime = Convert.ToDateTime(null)
                };
                _unitOfWork.TestResults.Create(testResult);
                _unitOfWork.Save();
            }
            return testResult.StartTime;
        }

        public TestResultDTO FinishTest(int testId, int userId, Dictionary<int, int[]> userAnswersDict)
        {
            TestResultEntity testResult = _unitOfWork.TestResults.FindOneBy(entity =>
                entity.TestId == testId && entity.UserId == userId && entity.EndTime == DateTime.MinValue);
            if (testResult == null)
                throw new TestNotStartedException();
            if (testResult.StartTime.AddMinutes(testResult.Test.Duration) <= DateTime.Now)
                throw new TestTimeRunOutException();
            testResult.EndTime = DateTime.Now;
            double score = 0;
            int totalScore = 0;
            testResult.Test.Questions.ToList().ForEach(question =>
            {
                if (!userAnswersDict.ContainsKey(question.Id))
                    throw new InvalidAnswerException($"Question {question.Id} is not found in passed answers");
                int[] userAnswers = userAnswersDict[question.Id];
                if (question.Type == QuestionType.SINGLE)
                {
                    if (userAnswers.Length > 1)
                        throw new InvalidAnswerException($"Question {question.Id} has only one answer");
                    if (userAnswers.Length == 1)
                    {
                        AnswerEntity correctAnswer = question.Answers.First(answer => answer.Correct);
                        if (userAnswers[0] == correctAnswer.Id)
                            score += question.score;
                    }
                }
                else
                {
                    if (!ListHelper.ContainsAll(question.Answers.Select(answer => answer.Id), userAnswers))
                        throw new InvalidAnswerException(
                            $"Question {question.Id} was responded with answers from another question");

                    List<AnswerEntity> correctAnswers = question.Answers.Where(answer => answer.Correct).ToList();
                    double scorePerAnswer = (double) question.score / correctAnswers.Count();
                    int countCorrect = 0, countIncorrect = 0;
                    foreach (int userAnswer in userAnswers)
                    {
                        if (question.Answers.First(answer => answer.Id == userAnswer).Correct)
                            countCorrect++;
                        else
                            countIncorrect++;
                    }

                    double questionScore = countCorrect * scorePerAnswer - countIncorrect * scorePerAnswer;
                    if (questionScore < 0)
                        questionScore = 0;
                    score += questionScore;
                }

                totalScore += question.score;
            });
            testResult.Score = score; 
            testResult.TotalScore = totalScore;
            _unitOfWork.TestResults.Update(testResult);
            _unitOfWork.Save();
            IMapper mapper = new MapperConfiguration(cfg =>{cfg.CreateMap<TestResultEntity, TestResultDTO>();}).CreateMapper();
            return mapper.Map<TestResultDTO>(testResult);
        }
    }
}