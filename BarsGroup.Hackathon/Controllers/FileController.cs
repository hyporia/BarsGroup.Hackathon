using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Models;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FileController : ControllerBase
	{
		private readonly IObjectStorage storage;
		private readonly IFileService fileService;

		public FileController(IObjectStorage storage, IFileService fileService)
		{
			this.storage = storage;
			this.fileService = fileService;
		}

		[HttpPost]
		[Authorize]
		public async Task<BaseResponse<SaveFileCommandResponse>> UpdloadFileAsync(IFormFile file)
		{
			using var command = new SaveFileCommand
			{
				Name = file.FileName,
				ContentType = file.ContentType,
				Stream = file.OpenReadStream()
			};
			var result = await fileService.SaveFileAsync(command);
			return new BaseResponse<SaveFileCommandResponse>
			{
				Result = result
			};
		}

		[HttpGet("{userId}")]
		public async Task<BaseResponse<GetByUserIdQueryResponse>> GetUserFilesAsync(Guid id)
		{
			var result = await fileService.GetUserFilesAsync(id);
			return new BaseResponse<GetByUserIdQueryResponse>
			{
				Result = result,
				Error = null
			};
		}
	}
}
