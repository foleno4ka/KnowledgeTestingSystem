using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeControlSystem.DAL.Enitties
{
    [Table("Questions")]
    public class QuestionEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Test")]
        public int TestId { get; set; }
        public virtual TestEntity Test { get; set; }

        [Required, Column(TypeName = "nvarchar"), StringLength(1024)]
        public string Text { get; set; }

        [Required]
        public int score { get; set; }

        [Required]
        public QuestionType Type { get; set; }
        ICollection<AnswerEntity> _answers;
        public virtual ICollection<AnswerEntity> Answers { get { return _answers ?? (this._answers = new HashSet<AnswerEntity>()); } set { _answers = value; } }
    }
}
