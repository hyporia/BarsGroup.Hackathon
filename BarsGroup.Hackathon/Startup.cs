using BarsGroup.Hackathon.Core;
using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.Db;
using BarsGroup.Hackathon.DB;
using BarsGroup.Hackathon.ObjectStorage;
using BarsGroup.Hackathon.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace BarsGroup.Hackathon
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			Env = env;
		}

		public IConfiguration Configuration { get; }

		/// <summary>
		/// Окружение
		/// </summary>
		private IWebHostEnvironment Env { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddCors();
			var sqlLoggerFactory = Env.IsDevelopment()
				? LoggerFactory.Create(builder => { builder.AddConsole(); })
				: null;

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
					.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

			services.AddPostgreSqlStorage(new PostgresDbOptions
			{
				ConnectionString = Configuration["App:DbConnectionString"]
			}, sqlLoggerFactory);

			services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<AppDbContext>();

			services.Configure<IdentityOptions>(options =>
			{
				// Default Password settings.
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 3;
				options.Password.RequiredUniqueChars = 1;
			});


			services.AddAwsS3Storage(options =>
			{
				options.BucketName = Configuration["App:StorageBucketName"];
				options.ServiceUrl = Configuration["App:StorageServiceUrl"];
				options.SecretAccessKey = Configuration["App:StorageSecretAccessKey"];
				options.AccessKeyId = Configuration["App:StorageAccessKeyId"];
			});

			if (long.TryParse(Configuration["App:MultipartBodyLengthLimit"], out var value))
				services.Configure<FormOptions>(x =>
				{
					x.MultipartBodyLengthLimit = value;
				});
			else throw new InvalidCastException("Некорректное значение в конфигурации App:MultipartBodyLengthLimit");

			services.AddSwaggerGen();


			services.AddScoped<IUserContext, UserContext>();

			services.AddDomain();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<ExceptionHandlingMiddleware>();
			app.UseCors(
			   options => options.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowCredentials().AllowAnyMethod()
		   );


			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseRouting();


			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
