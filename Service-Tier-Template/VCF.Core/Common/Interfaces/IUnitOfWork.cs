using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Common.Interfaces
{
	public interface IUnitOfWork
	{
		Task BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task<int> SaveChangesAsync();
	}
}
