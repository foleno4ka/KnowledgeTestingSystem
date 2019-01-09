using System;
using KnowledgeControlSystem.DAL.Enitties;

namespace KnowledgeControlSystem.DAL.Interfaces
{
 public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TestEntity> Tests { get; }
        //IGenericRepository<QuestionEntity> Questions { get; }
        //IGenericRepository<AnswerEntity> Answers { get; }
        IGenericRepository<TestResultEntity> TestResults { get; }
        IGenericRepository<CategoryEntity> Categories { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        void Save();
    }
}
