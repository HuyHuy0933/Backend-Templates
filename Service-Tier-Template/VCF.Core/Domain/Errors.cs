using Infrastructure.ResultUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Domain
{
	public static class Errors
	{
		private const string GET_LIST_MODULE = "get_list_module";

		public static class Module
		{
			public static Error GetListModuleDatabaseError => new Error(GET_LIST_MODULE, "Unable to get list module");
		}
	}
}
