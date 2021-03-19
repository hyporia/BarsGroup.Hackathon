using BarsGroup.Hackathon.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FileController : ControllerBase
	{
		private readonly IObjectStorage storage;

		public FileController(IObjectStorage storage)
		{
			this.storage = storage;
		}

		[HttpPost]
		public async Task<FileStreamResult> UpdloadFileAsync(IFormFile file)
		{
			var result = await storage.PutAsync(new Core.Models.ObjectPutParams { FileName = file.FileName, ContentType = file.ContentType }, file.OpenReadStream());
			var fileFromStorage = await storage.GetAsync(result.Key);
			return File(new MemoryStream(fileFromStorage), file.ContentType, file.FileName);
		}
	}
}
