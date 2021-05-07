using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Project.Core.Common.Interfaces;
using Project.Core.Entities;

namespace Project.Infra.Persistent.Repositories
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly DbContext _context;
		private IDbContextTransaction _currentTransaction;
		// private readonly ICurrentUserService _currentUserService;
		private bool disposed = false;

		public UnitOfWork(DbContext context)
		{
			_context = context;
			//_currentUserService = currentUserService;
			//_dateTime = dateTime;
		}

		public async Task BeginTransactionAsync()
		{
			if (_currentTransaction != null)
			{
				return;
			}

			_currentTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
		}

		public async Task CommitTransactionAsync()
		{
			try
			{
				await SaveChangesAsync().ConfigureAwait(false);

				_currentTransaction?.Commit();
			}
			catch
			{
				RollbackTransaction();
				throw;
			}
			finally
			{
				if (_currentTransaction != null)
				{
					_currentTransaction.Dispose();
					_currentTransaction = null;
				}
			}
		}

		public Task<int> SaveChangesAsync()
		{
			foreach (var entry in _context.ChangeTracker.Entries<EntityBase>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedDate = DateTime.UtcNow;
						break;
					case EntityState.Modified:
						entry.Entity.UpdatedDate = DateTime.UtcNow;
						break;
				}
			}

			return _context.SaveChangesAsync(CancellationToken.None);
		}

		public void RollbackTransaction()
		{
			try
			{
				_currentTransaction?.Rollback();
			}
			finally
			{
				if (_currentTransaction != null)
				{
					_currentTransaction.Dispose();
					_currentTransaction = null;
				}
			}
		}

		public void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			System.GC.SuppressFinalize(this);
		}
	}
}
