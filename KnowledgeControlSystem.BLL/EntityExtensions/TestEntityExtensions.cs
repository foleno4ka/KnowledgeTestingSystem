using System.Linq;
using KnowledgeControlSystem.DAL.Enitties;

namespace KnowledgeControlSystem.BLL.EntityExtensions
{
    public static class TestEntityExtensions
    {
        // TODO: not used
        public static int GetTotalScore(this TestEntity testEntity)
        {
            return testEntity.Questions.Sum(question => question.score);
        }
    }
}