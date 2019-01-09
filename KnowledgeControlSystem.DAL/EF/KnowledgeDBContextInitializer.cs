using System.Data.Entity;
using System.Linq;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using KnowledgeControlSystem.DAL.Identity;
using Microsoft.AspNet.Identity;

namespace KnowledgeControlSystem.DAL.EF
{
    public class KnowledgeDbContextInitializer : DropCreateDatabaseIfModelChanges<KnowledgeDBContext>
    {
        protected override void Seed(KnowledgeDBContext context)
        {
            const string adminRoleName = "Admin";
            string[] roleNames = {adminRoleName, "Moderator", "User"};

            ApplicationRoleManager roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
            foreach (var roleName in roleNames)
                if (!roleManager.RoleExists(roleName))
                    roleManager.Create(new IdentityRoleEntity() {Name = roleName});

            ApplicationUserManager userManager = new ApplicationUserManager(new ApplicationUserStore(context));
            const string adminUserName = "admin";
            if (userManager.FindByName(adminUserName) == null)
            {
                IdentityUserEntity adminUser = new IdentityUserEntity() {UserName = adminUserName};
                userManager.Create(adminUser, "admin123"); // min password size should be 6 symbols or more
                userManager.AddToRole(adminUser.Id, adminRoleName);
            }

            if (!context.Categories.Any())
                context.Categories.AddRange(new[]
                {
                    new CategoryEntity() {CategoryName = "Philosophy"},
                    new CategoryEntity() {CategoryName = "Sport"},
                    new CategoryEntity() {CategoryName = "Geography"}
                });
        }
    }
}