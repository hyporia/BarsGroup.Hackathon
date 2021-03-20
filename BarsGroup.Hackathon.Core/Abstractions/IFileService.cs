using BarsGroup.Hackathon.Core.Models.FileRequests.DeleteFile;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public interface IFileService
	{
		Task<GetByUserIdQueryResponse> GetByUserIdAsync(Guid id, bool onlyDeleted = false);
		Task<GetByUserIdQueryResponse> GetAsync(bool onlyDeleted = false);
		Task<SaveFileCommandResponse> SaveAsync(SaveFileCommand command);
		Task<DeleteFileByIdCommandResponse> DeleteByIdAsync(Guid id);
		Task<DeleteFileByIdCommandResponse> DeleteFromBucket(List<Guid> ids);
	}
}
