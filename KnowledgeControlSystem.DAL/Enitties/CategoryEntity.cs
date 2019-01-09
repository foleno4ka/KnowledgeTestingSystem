using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeControlSystem.DAL.Enitties
{
    [Table("Categories")]
    public class CategoryEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        ICollection<TestEntity> _tests;
        public virtual ICollection<TestEntity> Tests { get { return _tests ?? (this._tests = new HashSet<TestEntity>()); } }

    }
}
