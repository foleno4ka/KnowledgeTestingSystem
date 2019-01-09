using System.Collections.Generic;

namespace KnowledgeControlSystem.BLL.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public string Type { get; set; }
        ICollection<AnswerDTO> _answers;
        public virtual ICollection<AnswerDTO> Answers
        {
            get
            {
                return _answers ?? (this._answers = new HashSet<AnswerDTO>());
            }
            set
            {
                _answers = value;
            }
        }
    }
}
