using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.Core.Models;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.UserRequests.GetUsers;
using BarsGroup.Hackathon.Core.Models.UserRequests.LoginUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Web.Controllers
{
	[ApiController]
	[Authorize]
	[Route("[controller]")]
	public class UserController
	{
		private readonly IUserService userService;
		private readonly SignInManager<User> signInManager;
		private readonly UserManager<User> userManager;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IUserContext userContext;

		public UserController(
			IUserService userService,
			SignInManager<User> signInManager,
			UserManager<User> userManager,
			IHttpContextAccessor httpContextAccessor, IUserContext userContext)
		{
			this.userService = userService;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.httpContextAccessor = httpContextAccessor;
			this.userContext = userContext;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<BaseResponse<CreateUserResponse>> CreateUser([FromBody] CreateUserCommand command)
		{
			var result = await userService.CreateUserAsync(command);
			return new BaseResponse<CreateUserResponse>
			{
				Result = result
			};
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<BaseResponse<LoginUserCommandResponse>> LogInAsync(
			[FromBody] LoginUserCommand command)
		{
			var signInResult = await signInManager.PasswordSignInAsync(command.Login, command.Password, true, false);
			if (signInResult.Succeeded)
			{
				var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, command.Login) };
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);
				await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				var isAdmin = await userService.IsAdmin(command.Login);
				return new BaseResponse<LoginUserCommandResponse>
				{
					Result = new LoginUserCommandResponse
					{
						Success = true,
						IsAdmin = isAdmin
					}
				};
			}
			return new BaseResponse<LoginUserCommandResponse> { Error = "Неверный логин или пароль" };
		}

		[HttpPost("logout")]
		public async Task<BaseResponse<bool>> LogOutAsync()
		{
			await signInManager.SignOutAsync();
			await httpContextAccessor.HttpContext.SignOutAsync();
			return new BaseResponse<bool>
			{
				Result = true
			};
		}

		[HttpGet]
		public async Task<BaseResponse<GetUsersQueryResponse>> GetUsersAsync()
		{
			return new BaseResponse<GetUsersQueryResponse>
			{
				Result = await userService.GetUsersAsync()
			};
		}

		[HttpDelete("{id}")]
		public async Task<BaseResponse<bool>> DeleteUserAsync(Guid id)
		{
			var result = await userService.DeleteUserAsync(id);
			return new BaseResponse<bool>
			{
				Result = result
			};
		}
	}
}
