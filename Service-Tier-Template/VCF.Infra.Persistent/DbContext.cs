using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Project.Infra.Persistent.Common.Extensions;
using Project.Core.Entities;

namespace Project.Infra.Persistent
{
	public class DbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public DbSet<Core.Entities.Module> Modules { get; set; }
		public DbContext(
			DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				//other automated configurations left out
				if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
				{
					entityType.AddSoftDeleteQueryFilter();
				}
			}
		}
	}
}
