using System.Collections.Generic;

namespace KnowledgeControlSystem.BLL.DTOs
{
   public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        ICollection<TestDTO> _tests;
        public virtual ICollection<TestDTO> Tests { get { return _tests ?? (this._tests = new HashSet<TestDTO>()); } }

    }
}
