using System.Collections.Generic;
using System.Security.Claims;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.DAL.Interfaces
{
    public interface IUserRepository
    {
        IdentityResult Create(IdentityUserEntity user);
        IdentityResult Delete(IdentityUserEntity user);
        IdentityResult AddToRoles(int userId, IEnumerable<string> roles);
        IdentityResult AddToRoles(int userId, string role);
        IdentityResult RemoveFromRole(int userId, string role);
        IdentityResult Create(IdentityUserEntity user, string password);
        ClaimsIdentity CreateIdentity(IdentityUserEntity user, string authenticationType);
        IdentityResult Update(IdentityUserEntity user);
        IdentityUserEntity GetById(int id);
        IdentityUserEntity GetByLogin(string login);
        IdentityUserEntity GetByEmail(string login);
        IEnumerable<IdentityUserEntity> GetAllUsers();
        IEnumerable<string> GetRoles(int userId);
        IdentityUserEntity FindUser(string login, string password);

    }
}
