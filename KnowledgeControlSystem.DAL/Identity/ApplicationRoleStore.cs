using System.Data.Entity;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Identity
{
    public class ApplicationRoleStore : RoleStore<IdentityRoleEntity, int, IdentityUserRoleEntity>
    {
        public ApplicationRoleStore(DbContext context) : base(context)
        {
        }
    }
}