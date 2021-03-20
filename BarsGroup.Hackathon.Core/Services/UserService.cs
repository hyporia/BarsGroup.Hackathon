using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.Core.Exceptions;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IAppDbContext dbContext;
		private readonly UserManager<User> userManager;

		public UserService(IAppDbContext dbContext, UserManager<User> userManager)
		{
			this.dbContext = dbContext;
			this.userManager = userManager;
		}

		public async Task<CreateUserResponse> CreateUserAsync(CreateUserCommand command)
		{
			var user = new User { UserName = command.Login };
			var result = await userManager.CreateAsync(user, command.Password);
			if (result.Succeeded) return new CreateUserResponse { Id = user.Id };
			throw new ApplicationExceptionBase("Не удалось создать пользователя");
		}
	}
}
