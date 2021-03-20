using System;

namespace BarsGroup.Hackathon.Core.Entities
{
	public class File : BaseEntity
	{
		public int Size { get; set; }
		public string Name { get; set; }
		public string ContentType { get; set; }
		public string Address { get; set; }
		public bool IsDeleted { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
