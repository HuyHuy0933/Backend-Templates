using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.ResultUtil;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.Requests.Modules;
using Project.Application.Models.Responses.Modules;

namespace Project.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ModuleController : ApiController
	{
		public ModuleController()
		{
		}

		[HttpGet("list")]
		public async Task<Result<GetModuleListResponse>> GetList([FromQuery] GetModuleListRequest request)
		{
			return await Mediator.Send(request);
		}
	}
}
