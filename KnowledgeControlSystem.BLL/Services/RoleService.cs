using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.BLL.Services
{
    public class IRoleService : IService<RoleDTO>
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public IRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<IdentityRoleEntity, AnswerDTO>();
                cfg.CreateMap<AnswerDTO, IdentityRoleEntity>();
            }).CreateMapper();

        }

        public void Create(RoleDTO role)
        {
            _unitOfWork.Roles.Create(_mapper.Map<IdentityRoleEntity>(role));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Roles.Delete(_unitOfWork.Roles.Get(id));
            _unitOfWork.Save();
        }

        public IEnumerable<RoleDTO> FindBy(Expression<Func<RoleDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public RoleDTO Get(int id)
        {
           return _mapper.Map<RoleDTO>(_unitOfWork.Roles.Get(id));
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            return _unitOfWork.Roles.GetAll().Select(role => _mapper.Map<RoleDTO>(role));
        }

        public RoleDTO GetByName(string name)
        {
            return _mapper.Map<RoleDTO>(_unitOfWork.Roles.GetByName(name));
        }

        public void Update(RoleDTO dto)
        {
            _unitOfWork.Roles.Update(_mapper.Map<IdentityRoleEntity>(dto));
            _unitOfWork.Save();
        }
    }
}
