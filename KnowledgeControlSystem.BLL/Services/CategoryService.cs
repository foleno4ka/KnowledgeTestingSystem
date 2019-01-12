using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.BLL.Services
{
    public class CategoryService : IService<CategoryDTO>
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public CategoryService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<CategoryEntity, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, CategoryEntity>();
            }).CreateMapper();
        }

        public void Create(CategoryDTO category)
        {
            _unitOfWork.Categories.Create(_mapper.Map<CategoryEntity>(category));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Categories.Remove(_unitOfWork.Categories.Get(id));
            _unitOfWork.Save();
        }

        public CategoryDTO Get(int id)
        {
            return _mapper.Map<CategoryDTO>(_unitOfWork.Categories.Get(id));
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _unitOfWork.Categories.GetAll().Select(category => _mapper.Map<CategoryDTO>(category));
        }

        public void Update(CategoryDTO dto)
        {
            _unitOfWork.Categories.Update(_mapper.Map<CategoryEntity>(dto));
        }
    }
}
