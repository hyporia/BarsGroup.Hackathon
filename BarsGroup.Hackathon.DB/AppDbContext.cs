using Abdt.ElectronicArchive.AuthorizationService.Storage.PostgreSql.Configurations;
using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.DB
{
	/// <summary>
	/// Контекст EF Core для приложения
	/// </summary>
	public class AppDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>, IAppDbContext
	{
		public DbSet<File> Files { get; set; }

		public override DbSet<IdentityUser<Guid>> Users { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.HasDefaultSchema("hackathon");
			builder.ApplyConfigurationsFromAssembly(typeof(FileConfiguration).Assembly);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
			await SaveChangesAsync(true, cancellationToken);

		private static void UpdateTimestamp(EntityEntry entry)
		{
			var entity = entry.Entity;
			if (entity == null)
				return;

			if (entity is BaseEntity table)
				table.ModifyTimestamp = DateTime.UtcNow;
		}

		private static void OnSave(EntityEntry entityEntry)
		{
			if (entityEntry.State != EntityState.Unchanged)
				UpdateTimestamp(entityEntry);
		}
	}
}