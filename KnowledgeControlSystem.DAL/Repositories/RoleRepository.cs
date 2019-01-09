using System.Collections.Generic;
using System.Linq;
using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using KnowledgeControlSystem.DAL.Identity;
using KnowledgeControlSystem.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Repositories
{
    class RoleRepository : IRoleRepository
    {
        private readonly ApplicationRoleManager _roleManager;

        public RoleRepository(KnowledgeDBContext context)
        {
            _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
        }
        public IdentityResult Create(IdentityRoleEntity role) => _roleManager.Create(role);

        public IdentityResult Delete(IdentityRoleEntity role)=> _roleManager.Delete(role);

        public IEnumerable<IdentityRoleEntity> GetAll() => _roleManager.Roles.AsEnumerable();

        public IdentityRoleEntity Get(int id)=> _roleManager.FindById(id);

        public IdentityRoleEntity GetByName(string roleName) => _roleManager.FindByName(roleName);

        public IdentityResult Update(IdentityRoleEntity role) => _roleManager.Update(role);
    }
}
