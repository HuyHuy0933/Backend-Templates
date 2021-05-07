using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Project.Core.Common.Interfaces;
using Project.Infra.Persistent.Repositories;

namespace Project.Infra.Persistent.Common.Registras
{
	public static class InfrastructureRegistra
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<DbContext>(options =>
				options.UseSqlServer(
					configuration["ConnectionString"],
					b => b.MigrationsAssembly(typeof(DbContext).Assembly.FullName)));


			services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
