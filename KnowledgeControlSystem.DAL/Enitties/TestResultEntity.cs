using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;

namespace KnowledgeControlSystem.DAL.Enitties
{
    [Table("TestResults")]
    public class TestResultEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Test")]
        public int TestId { get; set; }
        public virtual TestEntity Test { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual IdentityUserEntity User { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime StartTime { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime EndTime { get; set; }
        
        public double Score { get; set; }
        
        public int TotalScore { get; set; }

    }
}
