using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace BarsGroup.Hackathon
{
	public class Program
	{
		private static IConfiguration Configuration { get; set; }

		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			Configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
				.Build();

			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					webBuilder.UseKestrel(opt =>
					{
						if (long.TryParse(Configuration["App:MaxRequestBodySize"], out var value))
							opt.Limits.MaxRequestBodySize = value;
						else
							throw new InvalidCastException("Некорректное значение конфигурации App:MaxRequestBodySize");

						opt.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
					});
				});
		}
	}
}
