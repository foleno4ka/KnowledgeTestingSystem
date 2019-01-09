using System.Data.Entity;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Identity
{
    public class ApplicationUserStore : UserStore<IdentityUserEntity, IdentityRoleEntity, int, IdentityUserLoginEntity,
        IdentityUserRoleEntity, IdentityUserClaimEntity>
    {
        public ApplicationUserStore(DbContext context) : base(context)
        {
        }
    }
}