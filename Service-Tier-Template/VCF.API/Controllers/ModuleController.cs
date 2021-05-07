using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.ListUtil.Extensions.IQueryable;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.Request.Modules;
using Project.Application.Models.Response.Modules;
using Project.Application.Services.Modules;
using Infrastructure.ResultUtil;

namespace Project.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ModuleController : Controller
	{
		private readonly IModuleService _moduleService;

		public ModuleController(
			IModuleService moduleService)
		{
			_moduleService = moduleService;
		}

		[HttpGet("list")]
		[ProducesResponseType(200, Type = typeof(Result<GetModuleListResponse>))]
		public IActionResult GetList([FromQuery] GetModuleListRequest request)
		{
			return Ok(_moduleService.GetSortedFilteredList(request));
		}
	}
}
