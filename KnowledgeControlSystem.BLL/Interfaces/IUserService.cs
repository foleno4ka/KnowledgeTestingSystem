using System.Collections.Generic;
using System.Security.Claims;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Infrastructure;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.BLL.Interfaces
{
    public interface IUserService
    {
        IdentityResult Create(UserDTO userDto);
        OperationDetails Delete(UserDTO user);
        UserDTO Get(int id);
        UserDTO GetByName(string name);
        IdentityResult AddToUserRoles(int userId, IEnumerable<string> roles);
        OperationDetails Update(UserDTO user);
        IEnumerable<UserDTO> GetAll();
        UserDTO FindUser(string login, string password);
        IEnumerable<string> GetRoles(int userId);
        //Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
