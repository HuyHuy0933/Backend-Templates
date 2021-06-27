using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(bool ignoreDeleteFilter = false);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool ignoreDeleteFilter = false);
        IQueryable<TEntity> GetInclude(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool ignoreDeleteFilter = false);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        Task<int> CommitAsync();
        void Insert(TEntity entity);
        void InsertRange(List<TEntity> entities);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool ignoreDeleteFilter = false);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool ignoreDeleteFilter = false);
        Task DeleteAsync(Guid Id, bool hardDeleted = false);
        Task DeleteRangeAsync(Guid[] Ids, bool hardDeleted = false);
    }
}
