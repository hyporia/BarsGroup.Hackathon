using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.Core.Enums;
using BarsGroup.Hackathon.Core.Exceptions;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.UserRequests.GetUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IAppDbContext dbContext;
		private readonly IUserContext userContext;
		private readonly UserManager<User> userManager;
		private readonly IObjectStorage objectStorage;

		public UserService(IAppDbContext dbContext, UserManager<User> userManager, IUserContext userContext, IObjectStorage objectStorage)
		{
			this.dbContext = dbContext;
			this.userManager = userManager;
			this.userContext = userContext;
			this.objectStorage = objectStorage;
		}

		public async Task<CreateUserResponse> CreateUserAsync(CreateUserCommand command)
		{
			var user = new User { UserName = command.Login };
			var result = await userManager.CreateAsync(user, command.Password);
			if (result.Succeeded)
			{
				if (command.Login.Contains("admin"))
				{
					var userRole = new IdentityUserRole<Guid>
					{
						RoleId = Users.Admin,
						UserId = user.Id
					};
					await dbContext.UserRoles.AddAsync(userRole);
					await dbContext.SaveChangesAsync();
				}

				return new CreateUserResponse { Id = user.Id };
			}
			throw new ApplicationExceptionBase("Не удалось создать пользователя");
		}

		public async Task<bool> DeleteUserAsync(Guid id)
		{

			var curUser = await userContext.GetCurrentUserAsync();
			var isAdmin = await dbContext.UserRoles
							.AnyAsync(x => x.UserId == curUser.Id && x.RoleId == Users.Admin);

			if (!isAdmin || id == curUser.Id) return false;

			var files = await dbContext.Files
				.Where(x => x.UserId == id)
				.ToListAsync();

			foreach (var file in files)
				await objectStorage.RemoveAsync(file.Address);

			dbContext.Files.RemoveRange(files);

			var user = await dbContext.Users
				.FirstOrDefaultAsync(x => x.Id == id);

			await userManager.DeleteAsync(user);

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<GetUsersQueryResponse> GetUsersAsync()
		{
			var user = await userContext.GetCurrentUserAsync();
			var isAdmin = await dbContext.UserRoles
							.AnyAsync(x => x.UserId == user.Id && x.RoleId == Users.Admin);

			if (!isAdmin) return new GetUsersQueryResponse();

			var users = await dbContext.Users.
				Include(x => x.Files)
				.Select(x => new GetUserQueryResponseItem
				{
					Id = x.Id,
					Login = x.UserName,
					DataSize = Math.Round((double)x.Files.Sum(x => x.Size) / 1048576, 2),
					FileNumber = x.Files.Count
				}
				).ToListAsync();

			return new GetUsersQueryResponse { Users = users };
		}

		public async Task<bool> IsAdmin(string userName)
		{
			var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
			if (user == null) return false;
			var role = await dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == Users.Admin);
			if (role != null) return true;
			return false;
		}
	}
}
