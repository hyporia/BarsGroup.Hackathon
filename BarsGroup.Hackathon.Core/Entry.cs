using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BarsGroup.Hackathon.Core
{
	public static class Entry
	{
		public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IFileService, FileService>();
			serviceCollection.AddTransient<IUserService, UserService>();
			return serviceCollection;
		}
	}
}
