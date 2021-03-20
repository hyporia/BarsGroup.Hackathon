using BarsGroup.Hackathon.Core.Models.FileRequests.DeleteFile;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand;
using System;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public interface IFileService
	{
		Task<GetByUserIdQueryResponse> GetByUserIdAsync(Guid id);
		Task<GetByUserIdQueryResponse> GetAsync();
		Task<SaveFileCommandResponse> SaveAsync(SaveFileCommand command);
		Task<DeleteFileByIdCommandResponse> DeleteByIdAsync(Guid id);
	}
}
