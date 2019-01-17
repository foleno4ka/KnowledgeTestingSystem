using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(KnowledgeDBContext context) : base(context)
        {
        }
    }
}