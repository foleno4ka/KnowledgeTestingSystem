using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using KnowledgeControlSystem.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        IdentityDbContext<IdentityUserEntity, IdentityRoleEntity, int, IdentityUserLoginEntity, IdentityUserRoleEntity, IdentityUserClaimEntity> context;
        IDbSet<T> _dbSet;
        public GenericRepository(IdentityDbContext<IdentityUserEntity, IdentityRoleEntity, int, IdentityUserLoginEntity, IdentityUserRoleEntity, IdentityUserClaimEntity> context)
        {
            this.context = context;
            _dbSet = context.Set<T>();
        }
        public void Create(T obj)
        {
            try
            {
                _dbSet.AddOrUpdate(obj);
            }
            catch (Exception exc)
            {

            }
        }

        public void Remove(T obj)
        {
            try
            {
                _dbSet.Remove(obj);
            }
            catch (Exception exc)
            {

            }
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

        public void Update(T obj)
        {
            _dbSet.AddOrUpdate(obj);
        }
    }
}
