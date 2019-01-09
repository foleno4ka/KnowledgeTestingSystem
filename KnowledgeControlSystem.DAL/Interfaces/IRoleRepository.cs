using System.Collections.Generic;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.DAL.Interfaces
{
    public interface IRoleRepository
    {
        IdentityResult Create(IdentityRoleEntity role);
        IEnumerable<IdentityRoleEntity> GetAll();
        IdentityRoleEntity Get(int id);
        IdentityRoleEntity GetByName(string roleName);
        IdentityResult Update(IdentityRoleEntity role);
        IdentityResult Delete(IdentityRoleEntity role);
    }
}
