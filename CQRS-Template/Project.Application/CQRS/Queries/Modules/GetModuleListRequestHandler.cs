using Infrastructure.ListUtil.Extensions.IQueryable;
using Infrastructure.ResultUtil;
using MediatR;
using Project.Application.Models.Requests.Modules;
using Project.Application.Models.Responses.Modules;
using Project.Core.Common.Interfaces;
using Project.Core.Domain;
using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.CQRS.Queries.Modules
{
	public class GetModuleListRequestHandler : IRequestHandler<GetModuleListRequest, Result<GetModuleListResponse>>
	{
		private readonly IRepository<Module> _moduleRepo;
		public GetModuleListRequestHandler(IRepository<Module> moduleRepo)
		{
			_moduleRepo = moduleRepo;
		}

		public async Task<Result<GetModuleListResponse>> Handle(GetModuleListRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var modules = _moduleRepo.Get().FilterSortAndGetPage(
						config: request.AsFilterSortPageConfig(),
						args: request,
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
