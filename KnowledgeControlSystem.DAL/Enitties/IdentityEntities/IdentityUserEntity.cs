using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Enitties.IdentityEntities
{
    [Table("Users")]
    public class IdentityUserEntity : IdentityUser<int, IdentityUserLoginEntity, IdentityUserRoleEntity, IdentityUserClaimEntity>, IUser<int>
    {
        /*[Key]
        public int Id { get; set; }
        [Required, Column(TypeName = "nvarchar"), StringLength(50)]
        public string LoginName { get; set; }
        [Required, Column(TypeName = "nvarchar"), StringLength(50)]
        public string Password { get; set; }
        [Required, Column(TypeName = "nvarchar"), StringLength(100)]
        public string DisplayName { get; set; }
        public UserRole Role { get; set; }*/

        ICollection<TestResultEntity> _testResults;
        public virtual ICollection<TestResultEntity> TestResults { get { return _testResults ?? (this._testResults = new HashSet<TestResultEntity>()); } }

    }
}
