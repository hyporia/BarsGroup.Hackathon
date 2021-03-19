using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Exceptions;
using BarsGroup.Hackathon.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BarsGroup.Hackathon.Db
{
	public static class Entry
	{
		/// <summary>
		/// Добавление зависимостей для работы с Postgresql
		/// </summary>
		/// <param name="services">Конфигурация зависимостей приложения</param>
		/// <param name="options">Конфиг подключения к Postgresql</param>
		/// <returns>Конфигурация зависимостей приложения</returns>
		public static IServiceCollection AddPostgreSqlStorage(
			this IServiceCollection services,
			PostgresDbOptions options,
			ILoggerFactory loggerFactory = null)
		{
			if (string.IsNullOrWhiteSpace(options?.ConnectionString))
				throw new InvalidConfigurationException(nameof(options.ConnectionString));

			services.AddDbContext<AppDbContext>(opt =>
			{
				if (loggerFactory != null)
					opt.UseLoggerFactory(loggerFactory);
				opt.UseSnakeCaseNamingConvention();
				opt.UseNpgsql(options.ConnectionString);
				opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});

			services.AddScoped<IAppDbContext>(x => x.GetRequiredService<AppDbContext>());


			return services;
		}
	}
}
