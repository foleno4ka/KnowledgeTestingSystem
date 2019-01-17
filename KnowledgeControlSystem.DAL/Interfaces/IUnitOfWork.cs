using System;
using KnowledgeControlSystem.DAL.Enitties;

namespace KnowledgeControlSystem.DAL.Interfaces
{
 public interface IUnitOfWork : IDisposable
    {
        ITestRepository Tests { get; }
        ITestResultRepository TestResults { get; }
        ICategoryRepository Categories { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        void Save();
    }
}
