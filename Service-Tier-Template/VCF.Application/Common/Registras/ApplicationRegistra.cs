using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Project.Application.Services.Modules;

namespace Project.Application.Common.Registras
{
	public static class ApplicationRegistra
	{
		public static IServiceCollection AddApplication(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddScoped<IModuleService, ModuleService>();
			return services;
		}
	}
}
