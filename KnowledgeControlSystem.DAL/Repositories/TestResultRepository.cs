using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.DAL.Repositories
{
    public class TestResultRepository: GenericRepository<TestResultEntity>, ITestResultRepository
    {
        public TestResultRepository(KnowledgeDBContext context) : base(context)
        {
        }
    }
}