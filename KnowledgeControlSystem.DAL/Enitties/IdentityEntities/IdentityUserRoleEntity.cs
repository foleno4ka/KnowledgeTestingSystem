using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Enitties.IdentityEntities
{
    public class IdentityUserRoleEntity : IdentityUserRole<int>
    {
        public virtual IdentityUserEntity User { get; set; }
        public virtual IdentityRoleEntity Role { get; set; }
    }
}