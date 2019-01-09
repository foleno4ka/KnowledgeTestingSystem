using System;
using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.DAL.Repositories
{
    public class KnowledgeUnitOfWork : IUnitOfWork
    {
        private readonly KnowledgeDBContext _context;
        GenericRepository<TestEntity> _tests;
        //GenericRepository<QuestionEntity> _questions;
        //GenericRepository<AnswerEntity> _answers;
        GenericRepository<TestResultEntity> _testResults;
        GenericRepository<CategoryEntity> _categories;
        UserRepository _users;
        RoleRepository _roles;

        public IGenericRepository<TestEntity> Tests => _tests ?? (_tests = new GenericRepository<TestEntity>(_context));
        //public IGenericRepository<QuestionEntity> Questions => _questions ?? (_questions = new GenericRepository<QuestionEntity>(_context));
        //public IGenericRepository<AnswerEntity> Answers => _answers ?? (_answers = new GenericRepository<AnswerEntity>(_context));
        public IGenericRepository<TestResultEntity> TestResults => _testResults ?? (_testResults = new GenericRepository<TestResultEntity>(_context));
        public IRoleRepository Roles => _roles ?? (_roles = new RoleRepository(_context));
        public IUserRepository Users => _users ?? (_users = new UserRepository(_context));
        public IGenericRepository<CategoryEntity> Categories => _categories ?? (_categories = new GenericRepository<CategoryEntity>(_context));

        public KnowledgeUnitOfWork(KnowledgeDBContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
                GC.SuppressFinalize(this);
        }
    }
}
