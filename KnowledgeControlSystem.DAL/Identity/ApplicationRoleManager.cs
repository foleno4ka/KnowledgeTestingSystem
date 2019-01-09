using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<IdentityRoleEntity, int>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRoleEntity, int> store)
                       : base(store)
        { }
    }
}
