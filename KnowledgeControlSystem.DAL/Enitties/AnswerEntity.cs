using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeControlSystem.DAL.Enitties
{
    [Table("Answers")]
    public class AnswerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, Column(TypeName = "nvarchar"), StringLength(1024)]
        public string Text { get; set; }
        [Required]
        public bool Correct { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual QuestionEntity Question { get; set; }
    }
}
