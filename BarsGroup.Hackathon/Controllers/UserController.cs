using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.Core.Models;
using BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId;
using BarsGroup.Hackathon.Core.Models.UserRequests.LoginUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController
	{
		private readonly IUserService userService;
		private readonly SignInManager<User> signInManager;
		private readonly UserManager<User> userManager;
		private readonly IHttpContextAccessor httpContextAccessor;

		public UserController(
			IUserService userService,
			SignInManager<User> signInManager,
			UserManager<User> userManager,
			IHttpContextAccessor httpContextAccessor)
		{
			this.userService = userService;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.httpContextAccessor = httpContextAccessor;
		}

		[HttpPost]
		public async Task<BaseResponse<CreateUserResponse>> CreateUser([FromBody] CreateUserCommand command)
		{
			var result = await userService.CreateUserAsync(command);
			return new BaseResponse<CreateUserResponse>
			{
				Result = result
			};
		}

		[HttpPost("login")]
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
				return new BaseResponse<LoginUserCommandResponse> { Result = new LoginUserCommandResponse { Success = true } };
			}
			return new BaseResponse<LoginUserCommandResponse> { Error = "Неверный логин или пароль" };
		}
	}
}
