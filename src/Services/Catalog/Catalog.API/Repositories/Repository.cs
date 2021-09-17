using Catalog.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;
        protected readonly DbContext context;

        public Repository(DbContext context)
        {
            _entities = context.Set<TEntity>();
            this.context = context;
        }

       
        public bool Add(TEntity entity)
        {
            _entities.Add(entity);
            return true;
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
             _entities.AddRange(entities);
            return 1;
        }

        public TEntity Find(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _entities.Where(expression);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities;
        }
    }
}
