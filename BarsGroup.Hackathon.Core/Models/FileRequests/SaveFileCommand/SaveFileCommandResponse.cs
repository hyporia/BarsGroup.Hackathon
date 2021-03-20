using System;

namespace BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand
{
	public class SaveFileCommandResponse
	{
		public Guid Id { get; set; }
		public int Size { get; set; }
		public string Name { get; set; }
		public string ContentType { get; set; }
	}
}
