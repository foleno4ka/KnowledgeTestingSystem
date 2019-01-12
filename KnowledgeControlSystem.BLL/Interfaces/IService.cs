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
        void Create(T obj);
        void Update(T dto);
        void Delete(int obj);
    }
}
