using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KnowledgeControlSystem.DAL.Interfaces
{
    public interface IGenericRepository<T> 
        where T : class
    {
        // TODO: change to IQueryable
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindOneBy(Expression<Func<T, bool>> predicate);
        T Get(int id);
        void Create(T obj);
        void Remove(T obj);
        void Update(T obj);
    }
}
