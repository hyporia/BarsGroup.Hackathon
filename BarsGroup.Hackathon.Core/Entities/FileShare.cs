using System;

namespace BarsGroup.Hackathon.Core.Entities
{
	public class FileShare : BaseEntity
	{
		public Guid FileId { get; set; }
		public Guid UserId { get; set; }

		public File File { get; set; }
		public User User { get; set; }
	}
}
