using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Find(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> expression);
        bool Add(TEntity entity);
        int AddRange(IEnumerable<TEntity> entities);

    }
}
