using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Entities
{
	public class EntityBase
	{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public bool IsDeleted { get; set; }
	}
}
