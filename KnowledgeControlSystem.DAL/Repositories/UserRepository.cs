using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using KnowledgeControlSystem.DAL.Identity;
using KnowledgeControlSystem.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationUserManager _userManager;

        public UserRepository(KnowledgeDBContext context)
        {
            _userManager = new ApplicationUserManager(new ApplicationUserStore(context));
        }

        public IdentityResult AddToRoles(int userId, IEnumerable<string> roles) =>
            _userManager.AddToRoles(userId, roles.ToArray());

        public IdentityResult Create(IdentityUserEntity user) => _userManager.Create(user);

        public IdentityResult Create(IdentityUserEntity user, string password) => _userManager.Create(user, password);

        public ClaimsIdentity CreateIdentity(IdentityUserEntity user, string authenticationType) =>
            _userManager.CreateIdentity(user, authenticationType);

        public IdentityResult Delete(IdentityUserEntity user) => _userManager.Delete(user);

        public IdentityUserEntity FindUser(string login, string password) => _userManager.Find(login, password);

        public IEnumerable<IdentityUserEntity> GetAllUsers() => _userManager.Users.AsEnumerable();

        public IdentityUserEntity GetByEmail(string login) => _userManager.FindByEmail(login);

        public IdentityUserEntity GetById(int id) => _userManager.FindById(id);

        public IdentityUserEntity GetByLogin(string login) => _userManager.FindByName(login);

        public IdentityResult Update(IdentityUserEntity user) => _userManager.Update(user);
        public IEnumerable<string> GetRoles(int userId) => _userManager.GetRoles(userId).AsEnumerable();
    }
}