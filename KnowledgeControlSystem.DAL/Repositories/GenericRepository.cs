using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Interfaces;

namespace KnowledgeControlSystem.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbSet<T> _dbSet;

        public GenericRepository(KnowledgeDBContext context)
        {
            _dbSet = context.Set<T>();
        }

        public void Create(T obj)
        {
            _dbSet.AddOrUpdate(obj);
        }

        public void Remove(T obj)
        {           
            _dbSet.Remove(obj);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T FindOneBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual void Update(T obj)
        {
            _dbSet.AddOrUpdate(obj);
        }
    }
}