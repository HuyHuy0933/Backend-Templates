using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Project.Core.Entities;

namespace Project.Application.Models.Response.Modules
{
	public class GetModuleListResponse
	{
		public ModuleListResponse[] ModuleList { get; set; }
	}

	public class ModuleListResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public static Expression<Func<Module, ModuleListResponse>> GetFromModule =
			(Module p) => new ModuleListResponse
			{
				Id = p.Id,
				Name = p.Name
			};
	}
}
