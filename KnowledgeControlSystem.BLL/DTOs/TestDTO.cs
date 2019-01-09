using System;
using System.Collections.Generic;

namespace KnowledgeControlSystem.BLL.DTOs
{
   public class TestDTO
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        ICollection<QuestionDTO> _questions;
        public ICollection<QuestionDTO> Questions { get { return _questions ?? (this._questions = new HashSet<QuestionDTO>()); } set { _questions = value; } }
        public int CategoryId { get; set; }
    }
}
