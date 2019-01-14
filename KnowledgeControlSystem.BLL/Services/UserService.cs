using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Infrastructure;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using KnowledgeControlSystem.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<IdentityUserEntity, UserDTO>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore());
                cfg.CreateMap<IdentityUserRoleEntity, string>().ConvertUsing(src => src.Role.Name);
                cfg.CreateMap<UserDTO, IdentityUserEntity>()
                    .ForMember(dest => dest.Roles, opt => opt.Ignore());
            }).CreateMapper();
        }

        public UserDTO Get(int id) => _mapper.Map<UserDTO>(_unitOfWork.Users.GetById(id));

        public IEnumerable<UserDTO> GetAll()
        {
            List<IdentityUserEntity> identityUsers = _unitOfWork.Users.GetAllUsers().ToList();
            IEnumerable<UserDTO> users = identityUsers.Select(user => _mapper.Map<UserDTO>(user));
            return users;
        }

        public OperationDetails Delete(UserDTO user)
        {
            IdentityUserEntity userEntity = _mapper.Map<IdentityUserEntity>(_unitOfWork.Users.GetById(user.Id));
            if (userEntity == null)
                return new OperationDetails(false, $"{user.UserName} is not found!", "");
            _unitOfWork.Users.Delete(userEntity);
            return new OperationDetails(true, "User was deleted successful", "");
        }

        public OperationDetails Update(UserDTO user)
        {
            IdentityUserEntity userControl = _unitOfWork.Users.GetById(user.Id);
            if (userControl == null)
                return new OperationDetails(false, $"{user.UserName} is not found!", "");
            _unitOfWork.Users.Update(userControl);
            _unitOfWork.Save();
            return new OperationDetails(true, "User Information was updated", "");
        }

        public IdentityResult Create(UserDTO userDto)
        {
            var user = _unitOfWork.Users.GetByEmail(userDto.Email);

            if (user != null)
                return new IdentityResult("User with such login exists");
            IdentityUserEntity newUser = new IdentityUserEntity {Email = userDto.Email, UserName = userDto.UserName};
            _unitOfWork.Users.Create(newUser, userDto.Password);
            _unitOfWork.Save();
            return IdentityResult.Success;
        }

        public UserDTO FindUser(string login, string password) =>
            _mapper.Map<UserDTO>(_unitOfWork.Users.FindUser(login, password));

        public UserDTO GetByName(string name) =>
            _mapper.Map<UserDTO>(_unitOfWork.Users.GetByLogin(name));

        public IdentityResult AddToUserRoles(int userId, IEnumerable<string> roles)
        {
            var result = _unitOfWork.Users.AddToRoles(userId, roles);
            if (result.Succeeded)
                _unitOfWork.Save();
            return result;
        }
        
        public IdentityResult AddToUserRoles(int userId, string role)
        {
            var result = _unitOfWork.Users.AddToRoles(userId, role);
            if (result.Succeeded)
                _unitOfWork.Save();
            return result;
        }
        public IdentityResult DeleteFromRole(int userId, string role)
        {
            var result = _unitOfWork.Users.RemoveFromRole(userId, role);
            if (result.Succeeded)
                _unitOfWork.Save();
            return result;
        }

        public IEnumerable<string> GetRoles(int userId)
        {
            return _unitOfWork.Users.GetRoles(userId);
        }
    }
}