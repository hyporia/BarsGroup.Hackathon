using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.UserRequests.GetUsers;
using System;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public interface IUserService
	{
		Task<CreateUserResponse> CreateUserAsync(CreateUserCommand command);

		Task<GetUsersQueryResponse> GetUsersAsync();
		Task<bool> DeleteUserAsync(Guid id);

		Task<bool> IsAdmin(string userId);
	}
}
