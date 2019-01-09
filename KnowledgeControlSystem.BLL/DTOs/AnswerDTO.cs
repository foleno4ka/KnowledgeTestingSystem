namespace KnowledgeControlSystem.BLL.DTOs
{
   public class AnswerDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }        
        public bool Correct { get; set; }
        public int QuestionId { get; set; }
    }
}
