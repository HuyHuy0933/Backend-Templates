using Infrastructure.ListUtil.Extensions.IQueryable;
using Infrastructure.ResultUtil;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Project.Application.Models.Request.Modules;
using Project.Application.Models.Response.Modules;
using Project.Core.Common.Interfaces;
using Project.Core.Domain;
using Project.Core.Entities;

namespace Project.Application.Services.Modules
{
	public class ModuleService : IModuleService
	{
		private readonly IRepository<Module> _moduleRepo;
		public ModuleService(IRepository<Module> moduleRepo)
		{
			_moduleRepo = moduleRepo;
		}

		public Result<GetModuleListResponse> GetSortedFilteredList(GetModuleListRequest input)
		{
			try
			{
				var modules = _moduleRepo.GetAsync().FilterSortAndGetPage(
						config: input.AsFilterSortPageConfig(),
						args: input,
						itemCount: out int itemCount);

				var results = new GetModuleListResponse
				{
					ModuleList = modules.Select(ModuleListResponse.GetFromModule).ToArray()
				};

				return Result<GetModuleListResponse>.Ok(results, itemCount);
			}
			catch (SqlException)
			{
				return Errors.Module.GetListModuleDatabaseError;
			}
		}
	}
}
