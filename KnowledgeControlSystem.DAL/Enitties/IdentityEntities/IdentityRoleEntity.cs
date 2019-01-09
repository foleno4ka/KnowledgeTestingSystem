using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Enitties.IdentityEntities
{
    public class IdentityRoleEntity : IdentityRole<int, IdentityUserRoleEntity>
    {
        /*public bool isAdminRole()
        {
            return "ADMIN" == this.Name;
        }
        public UserRole convertToUserRole()
        {
            if (this.Name == "ADMIN")
                return UserRole.ADMIN;
            if (this.Name == "MANAGER")
                return UserRole.MANAGER;
            if (this.Name == "STANDARD")
                return UserRole.STANDARD;
            throw new Exception();
        }*/
    }
}
