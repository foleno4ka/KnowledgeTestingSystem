using System;
using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.DAL.Repositories
{
    public class KnowledgeUnitOfWork : IUnitOfWork
    {
        private readonly KnowledgeDBContext _context;
        private bool _isDisposed;

        public ITestRepository Tests { get; }
        public ITestResultRepository TestResults { get; }
        public IRoleRepository Roles { get; }
        public IUserRepository Users { get; }
        public ICategoryRepository Categories { get; }

        public KnowledgeUnitOfWork(KnowledgeDBContext context, ITestRepository tests, ITestResultRepository testResults,
            ICategoryRepository categories, IUserRepository users, IRoleRepository roles)
        {
            _context = context;
            Tests = tests;
            TestResults = testResults;
            Categories = categories;
            Users = users;
            Roles = roles;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}