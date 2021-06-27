using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Project.Application.Models.Responses.Modules
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
