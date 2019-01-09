using System.Collections.Generic;
using KnowledgeControlSystem.BLL.DTOs;

namespace KnowledgeControlSystem.WebAPІ.Models
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        ICollection<QuestionDTO> _questions;
        public virtual ICollection<QuestionDTO> Questions { get { return _questions ?? (this._questions = new HashSet<QuestionDTO>()); } set { _questions = value; } }
        public int CategoryId { get; set; }
    }
}