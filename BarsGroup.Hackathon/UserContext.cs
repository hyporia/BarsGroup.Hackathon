using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Web
{
	public class UserContext : IUserContext
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<User> userManager;

		public UserContext(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
		}

		public async Task<User> GetCurrentUserAsync() => await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);


	}
}
