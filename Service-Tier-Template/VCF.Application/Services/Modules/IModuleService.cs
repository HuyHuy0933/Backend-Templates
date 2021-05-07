using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Application.Models.Request.Modules;
using Project.Application.Models.Response.Modules;
using Project.Core.Entities;
using Infrastructure.ResultUtil;

namespace Project.Application.Services.Modules
{
	public interface IModuleService
	{
		Result<GetModuleListResponse> GetSortedFilteredList(GetModuleListRequest input);
	}
}
