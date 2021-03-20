﻿using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Services
{
	public class FileService : IFileService
	{
		private readonly IAppDbContext dbContext;
		private readonly IObjectStorage objectStorage;

		public FileService(IAppDbContext dbContext, IObjectStorage objectStorage)
		{
			this.dbContext = dbContext;
			this.objectStorage = objectStorage;
		}

		public async Task<SaveFileCommandResponse> SaveFileAsync(SaveFileCommand command)
		{
			var putResult = await objectStorage.PutAsync(
				new Models.ObjectPutParams
				{
					ContentType = command.ContentType,
					FileName = command.Name
				}, command.Stream);
			var user = await dbContext.Users.FirstOrDefaultAsync();

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

		public async Task<GetByUserIdQueryResponse> GetUserFilesAsync(Guid id)
		{
			var files = await dbContext.Files.Where(x => x.UserId == id).Select(x =>
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
	}
}