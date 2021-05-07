using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Project.Core.Common.Interfaces;
using Project.Core.Entities;

namespace Project.Infra.Persistent.Repositories
{
	public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
	{
		protected DbContext _dbContext;
		protected readonly DbSet<TEntity> _dbSet;
		private readonly IUnitOfWork _unitOfWork;

		public EFRepository(DbContext dbContext, IUnitOfWork unitOfWork)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<TEntity>();
			_unitOfWork = unitOfWork;
		}
		public async Task<int> CommitAsync()
		{
			return await _unitOfWork.SaveChangesAsync();
		}

		public async Task<TEntity> FindAsync(
			Expression<Func<TEntity, bool>> predicate, 
			bool ignoreDeleteFilter = false)
		{
			if (predicate == null)
			{
				return null;
			}

			if (ignoreDeleteFilter)
			{
				return await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(predicate);
			}

			return await _dbSet.FirstOrDefaultAsync(predicate);
		}

		public async Task<TEntity> FindAsync(
			Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, 
			bool ignoreDeleteFilter = false)
		{
			IQueryable<TEntity> query = _dbSet;

			if (predicate == null)
			{
				return null;
			}

			if (include != null)
			{
				query = include(query);
			}

			if (ignoreDeleteFilter)
			{
				return await query.IgnoreQueryFilters().FirstOrDefaultAsync(predicate);
			}

			return await query.FirstOrDefaultAsync(predicate);
		}

		public IQueryable<TEntity> GetAsync(bool ignoreDeleteFilter = false)
		{
			IQueryable<TEntity> query = _dbSet;

			if (ignoreDeleteFilter)
			{
				return query.IgnoreQueryFilters();
			}

			return query;
		}

		public IQueryable<TEntity> GetAsync(
			Expression<Func<TEntity, bool>> predicate, 
			bool ignoreDeleteFilter = false)
		{
			IQueryable<TEntity> query = _dbSet;

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (ignoreDeleteFilter)
			{
				return query.IgnoreQueryFilters().Where(predicate);
			}

			return query;
		}

		public IQueryable<TEntity> GetInclude(
			Expression<Func<TEntity, bool>> predicate, 
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, 
			bool ignoreDeleteFilter = false)
		{
			IQueryable<TEntity> query = _dbSet;

			if (include != null)
			{
				query = include(query);
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (ignoreDeleteFilter)
			{
				return query.IgnoreQueryFilters().Where(predicate);
			}

			return query;
		}

		public void Insert(TEntity entity)
		{
			if (entity == null) return;

			_dbSet.Add(entity);
		}

		public void InsertRange(List<TEntity> entities)
		{
			if (entities == null || entities.Count == 0)
			{
				return;
			}

			foreach (var entity in entities)
			{
				_dbSet.Add(entity);
			}
		}

		public void Update(TEntity entity)
		{
			if (entity == null) return;

			_dbSet.Update(entity);
		}

		public void UpdateRange(List<TEntity> entities)
		{
			if (entities == null || entities.Count == 0)
			{
				return;
			}

			foreach (var entity in entities)
			{
				_dbSet.Update(entity);
			}
		}

		public async Task DeleteAsync(Guid Id, bool hardDeleted = false)
		{
			if (Id == null) return;

			var entity = await _dbSet.FindAsync(Id);

			if (entity == null) return;

			if (hardDeleted) _dbSet.Remove(entity);

			entity.IsDeleted = true;
		}

		public async Task DeleteRangeAsync(Guid[] Ids, bool hardDeleted = false)
		{
			if (Ids == null) return;

			List<TEntity> entities = new List<TEntity>();

			foreach (var id in Ids)
			{
				var entity = await _dbSet.FindAsync(id);
				if (entity != null)
				{
					entities.Add(entity);
				}
			}

			if (entities == null) return;

			if (hardDeleted) _dbSet.RemoveRange(entities);

			foreach (var entity in entities)
			{
				entity.IsDeleted = true;
			}
		}
	}
}
