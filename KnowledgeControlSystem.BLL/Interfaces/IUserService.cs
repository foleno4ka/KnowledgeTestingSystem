using System.Collections.Generic;
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
        IdentityResult AddToUserRoles(int userId, string role);
        IdentityResult DeleteFromRole(int userId, string role);
        OperationDetails Update(UserDTO user);
        IEnumerable<UserDTO> GetAll();
        UserDTO FindUser(string login, string password);
        IEnumerable<string> GetRoles(int userId);
    }
}
