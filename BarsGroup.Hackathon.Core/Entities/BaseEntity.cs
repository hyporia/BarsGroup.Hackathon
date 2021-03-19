using System;

namespace BarsGroup.Hackathon.Core.Entities
{
	public class BaseEntity
	{
		public Guid Id { get; set; }
		public DateTime ModifyTimestamp { get; set; }
		public DateTime CreatedTimestamp { get; set; }
	}
}
