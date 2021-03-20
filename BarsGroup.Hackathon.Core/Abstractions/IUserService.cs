using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public interface IUserService
	{
		Task<CreateUserResponse> CreateUserAsync(CreateUserCommand command);
	}
}
