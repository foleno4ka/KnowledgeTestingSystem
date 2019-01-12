using System.Data.Entity.Migrations;
using System.Linq;
using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Enitties;

namespace KnowledgeControlSystem.DAL.Repositories
{
    public class TestRepository : GenericRepository<TestEntity>
    {
        private readonly KnowledgeDBContext _context;

        public TestRepository(KnowledgeDBContext context) : base(context)
        {
            _context = context;
        }

        public override void Update(TestEntity entity)
        {
            if (entity.Questions.Count() != 0)
            {
                foreach (QuestionEntity question in entity.Questions)
                {
                    if (question != null)
                    {
                        if (question.Test == null)
                            question.TestId = entity.Id;
                        _context.Set<QuestionEntity>().AddOrUpdate(question);
                        foreach (AnswerEntity answer in question.Answers)
                        {
                            if (answer.Question == null)
                                answer.QuestionId = question.Id;
                            _context.Set<AnswerEntity>().AddOrUpdate(answer);
                        }
                    }
                }
            }

            _context.Set<TestEntity>().AddOrUpdate(entity);
        }
    }
}