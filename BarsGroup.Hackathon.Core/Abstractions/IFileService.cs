using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.FileRequests.SaveFileCommand;
using System;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public interface IFileService
	{
		Task<GetByUserIdQueryResponse> GetUserFilesAsync(Guid id);

		Task<SaveFileCommandResponse> SaveFileAsync(SaveFileCommand command);
	}
}
