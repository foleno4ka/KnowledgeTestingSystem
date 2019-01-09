using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeControlSystem.DAL.Enitties
{
    [Table("Tests")]
    public class TestEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Duration { get; set; }
        public string Name { get; set; }
        ICollection<QuestionEntity> _questions;
        public virtual ICollection<QuestionEntity> Questions { get { return _questions ?? (_questions = new HashSet<QuestionEntity>()); } set { _questions = value; } }
        ICollection<TestResultEntity> _testResults;
        public virtual ICollection<TestResultEntity> TestResults { get { return _testResults ?? (_testResults = new HashSet<TestResultEntity>()); } }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}
