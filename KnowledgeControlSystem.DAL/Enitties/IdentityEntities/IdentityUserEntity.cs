using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Enitties.IdentityEntities
{
    [Table("Users")]
    public class IdentityUserEntity : IdentityUser<int, IdentityUserLoginEntity, IdentityUserRoleEntity, IdentityUserClaimEntity>, IUser<int>
    {
        ICollection<TestResultEntity> _testResults;
        public virtual ICollection<TestResultEntity> TestResults { get { return _testResults ?? (this._testResults = new HashSet<TestResultEntity>()); } }

    }
}
