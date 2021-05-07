using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Application.Models.Request
{
	public class BaseListRequest
	{
		public int pageNumber { get; set; } = 0;
		public int pageSize { get; set; } = 10;
		public string searchText { get; set; }
		public List<string> sortCriteria { get; set; }
	}
}
