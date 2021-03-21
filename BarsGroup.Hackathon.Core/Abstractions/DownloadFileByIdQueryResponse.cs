using System.IO;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public class DownloadFileByIdQueryResponse
	{
		public Stream Stream { get; set; }
		public string Name { get; set; }
		public string ContentType { get; set; }
	}
}