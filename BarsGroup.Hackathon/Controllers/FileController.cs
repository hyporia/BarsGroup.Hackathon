using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.Core.Models;
using BarsGroup.Hackathon.Core.Models.FileRequests.DeleteFile;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class FileController : ControllerBase
	{
		private readonly IFileService fileService;
		private readonly UserManager<User> userManager;

		public FileController(IFileService fileService, UserManager<User> userManager)
		{
			this.fileService = fileService;
			this.userManager = userManager;
		}

		[HttpPost]
		public async Task<BaseResponse<SaveFileCommandResponse>> UpdloadFileAsync(IFormFile file)
		{
			var userId = await userManager.GetUserAsync(User);
			using var command = new SaveFileCommand
			{
				Name = file.FileName,
				ContentType = file.ContentType,
				Stream = file.OpenReadStream()
			};
			var result = await fileService.SaveAsync(command);
			return new BaseResponse<SaveFileCommandResponse>
			{
				Result = result
			};
		}

		[HttpGet]
		public async Task<BaseResponse<GetByUserIdQueryResponse>> GetAsync([FromQuery] Guid? userId)
		{
			if (userId.HasValue)
				return new BaseResponse<GetByUserIdQueryResponse>
				{
					Result = await fileService.GetByUserIdAsync(userId.Value),
					Error = null
				};

			return new BaseResponse<GetByUserIdQueryResponse>
			{
				Result = await fileService.GetAsync(),
				Error = null
			};
		}

		[HttpDelete("{id}")]
		public async Task<BaseResponse<DeleteFileByIdCommandResponse>> DeleteFileByIdAsync(Guid id)
		{
			var result = await fileService.DeleteByIdAsync(id);
			return new BaseResponse<DeleteFileByIdCommandResponse>
			{
				Result = result,
				Error = null
			};
		}
	}
}
