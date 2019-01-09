using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.DAL.Identity
{
    public class ApplicationUserManager : UserManager<IdentityUserEntity, int>
    {
        public ApplicationUserManager(IUserStore<IdentityUserEntity, int> store)
               : base(store)
        { }
    }
}
