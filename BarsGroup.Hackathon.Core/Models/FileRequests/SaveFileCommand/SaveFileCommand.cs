using System;
using System.IO;

namespace BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand
{
	public class SaveFileCommand : IDisposable
	{
		public Stream Stream { get; set; }

		public string ContentType { get; set; }

		public string Name { get; set; }

		public void Dispose()
		{
			Stream?.Dispose();
		}
	}
}
