using BarsGroup.Hackathon.Core.Entities;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public interface IUserContext
	{
		Task<User> GetCurrentUserAsync();
	}
}
