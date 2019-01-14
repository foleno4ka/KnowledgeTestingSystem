using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.BLL.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<TestResultEntity, TestResultDTO>()
                    .ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.Test.Name))
                    .ForMember(dest => dest.MaxDuration, opt => opt.MapFrom(src => src.Test.Duration));
                cfg.CreateMap<TestResultDTO, TestResultEntity>();
            }).CreateMapper();
        }

        public void Create(TestResultDTO testResult)
        {
            _unitOfWork.TestResults.Create(_mapper.Map<TestResultEntity>(testResult));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.TestResults.Remove(_unitOfWork.TestResults.Get(id));
            _unitOfWork.Save();
        }

        public List<TestResultDTO> FindByUser(int userId)
        {
            return _unitOfWork.TestResults.FindBy(test => test.UserId == userId)
                .Select(testResult => _mapper.Map<TestResultDTO>(testResult)).ToList();
        }

        public TestResultDTO Get(int id)
        {
            return _mapper.Map<TestResultDTO>(_unitOfWork.TestResults.Get(id));
        }

        public IEnumerable<TestResultDTO> GetAll()
        {
            return _unitOfWork.TestResults.GetAll().Select(testResult => _mapper.Map<TestResultDTO>(testResult));
        }

        public void Update(TestResultDTO dto)
        {
            _unitOfWork.TestResults.Update(_mapper.Map<TestResultEntity>(dto));
            _unitOfWork.Save();
        }
    }
}