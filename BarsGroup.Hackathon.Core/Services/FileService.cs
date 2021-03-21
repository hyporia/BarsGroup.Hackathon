using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.Core.Enums;
using BarsGroup.Hackathon.Core.Exceptions;
using BarsGroup.Hackathon.Core.Models.FileRequests.DeleteFile;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Services
{
	public class FileService : IFileService
	{
		private readonly IAppDbContext dbContext;
		private readonly IObjectStorage objectStorage;
		private readonly IUserContext userContext;

		public FileService(IAppDbContext dbContext, IObjectStorage objectStorage, UserManager<User> userManager, IUserContext userContext)
		{
			this.dbContext = dbContext;
			this.objectStorage = objectStorage;
			this.userContext = userContext;
		}

		public async Task<SaveFileCommandResponse> SaveAsync(SaveFileCommand command)
		{
			var user = await userContext.GetCurrentUserAsync();
			var dataSize = await dbContext.Files.Where(x => x.UserId == user.Id).SumAsync(x => (double)x.Size / 1048576);
			if (dataSize + (double)command.Stream.Length / 1048576 > 100) return null;
			var putResult = await objectStorage.PutAsync(
				new Models.ObjectPutParams
				{
					ContentType = command.ContentType,
					FileName = command.Name
				}, command.Stream);

			var file = new File
			{
				Address = putResult.Key,
				Name = command.Name,
				Size = (int)command.Stream.Length,
				ContentType = command.ContentType,
				UserId = user.Id
			};
			await dbContext.Files.AddAsync(file);
			await dbContext.SaveChangesAsync();
			return new SaveFileCommandResponse
			{
				Id = file.Id,
				ContentType = file.ContentType,
				Name = file.Name,
				Size = file.Size
			};
		}

		public async Task<GetByUserIdQueryResponse> GetByUserIdAsync(Guid id, bool onlyDeleted = false)
		{
			var user = await userContext.GetCurrentUserAsync();
			var isAdmin = await dbContext.Roles.AnyAsync(x => x.Name == "admin");
			if (!isAdmin) return new GetByUserIdQueryResponse { Files = new List<GetByUserIdQueryResponseItem>() };
			var files = await dbContext.Files
				.Where(x => x.IsDeleted == onlyDeleted)
				.Where(x => x.UserId == id).Select(x =>
				new GetByUserIdQueryResponseItem
				{
					Id = x.Id,
					Address = x.Address,
					ContentType = x.ContentType,
					Name = x.Name,
					Size = x.Size
				}
			).ToListAsync();

			return new GetByUserIdQueryResponse { Files = files };
		}

		public async Task<DeleteFileByIdCommandResponse> DeleteByIdAsync(Guid id)
		{
			var file = await dbContext.Files.FirstOrDefaultAsync(x => x.Id == id)
				?? throw new NotFoundException("Не удалось найти файл с указанным идентификатором");
			var user = await userContext.GetCurrentUserAsync();
			var isAdmin = await dbContext.Roles.AnyAsync(x => x.Name == "admin");

			if (!isAdmin && file.UserId != user.Id) throw new NotFoundException("Не удалось найти файл с указанным идентификатором");
			file.IsDeleted = true;
			dbContext.Files.Update(file);
			await dbContext.SaveChangesAsync();
			return new DeleteFileByIdCommandResponse { Success = true };
		}

		public async Task<GetByUserIdQueryResponse> GetAsync(bool onlyDeleted = false)
		{
			var user = await userContext.GetCurrentUserAsync();
			var files = await dbContext.Files
				.Where(x => x.UserId == user.Id)
				.Where(x => x.IsDeleted == onlyDeleted)
				.Select(x =>
				new GetByUserIdQueryResponseItem
				{
					Id = x.Id,
					Address = x.Address,
					ContentType = x.ContentType,
					Name = x.Name,
					Size = x.Size
				}
			)
				.OrderByDescending(x => x.Size).ToListAsync();

			return new GetByUserIdQueryResponse { Files = files };

		}

		public async Task<DeleteFileByIdCommandResponse> DeleteFromBucket(List<Guid> ids)
		{
			ids ??= new List<Guid>();
			var user = await userContext.GetCurrentUserAsync();
			var isAdmin = await dbContext.UserRoles
				.AnyAsync(x => x.UserId == user.Id && x.RoleId == Users.Admin);
			if (!isAdmin)
			{
				var filesToDelete = await dbContext.Files
					.Where(x => x.IsDeleted)
					.Where(x => x.UserId == user.Id)
					.Where(x => !ids.Any() || ids.Contains(x.Id))
					.ToListAsync();
				foreach (var file in filesToDelete)
				{
					await objectStorage.RemoveAsync(file.Address);
				}
				dbContext.Files.RemoveRange(filesToDelete);
				await dbContext.SaveChangesAsync();
			}
			else
			{
				var filesToDelete = await dbContext.Files
					.Where(x => x.IsDeleted)
					.Where(x => !ids.Any() || ids.Contains(x.Id)).ToListAsync();
				foreach (var file in filesToDelete)
				{
					await objectStorage.RemoveAsync(file.Address);
				}
				dbContext.Files.RemoveRange(filesToDelete);
				await dbContext.SaveChangesAsync();
			}
			return new DeleteFileByIdCommandResponse { Success = true };
		}

		public async Task<DownloadFileByIdQueryResponse> DownloadFileById(Guid id)
		{
			var user = await userContext.GetCurrentUserAsync();
			var file = await dbContext.Files.FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id)
				?? throw new NotFoundException("Не удалось найти файл с указанным идентификатором");

			var stream = await objectStorage.GetStreamAsync(file.Address);
			return new DownloadFileByIdQueryResponse
			{
				ContentType = file.ContentType,
				Name = file.Name,
				Stream = stream
			};
		}
	}
}
