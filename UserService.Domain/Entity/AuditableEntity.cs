using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entity
{
	public class AuditableEntity
	{
		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public string CreatedBy { get; set; }
		public string? UpdatedBy { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
