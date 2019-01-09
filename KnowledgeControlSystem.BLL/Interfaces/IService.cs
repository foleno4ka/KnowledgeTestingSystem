using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KnowledgeControlSystem.BLL.Interfaces
{
   public interface IService<T>
        where T:class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T GetByName(string name);
        void Create(T obj);
        void Update(T dto);
        void Delete(int obj);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
