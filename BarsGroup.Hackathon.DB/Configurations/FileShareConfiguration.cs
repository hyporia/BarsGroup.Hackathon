using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.DB.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abdt.ElectronicArchive.AuthorizationService.Storage.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация <see cref="Privilege"/>
	/// </summary>
	internal class FileShareConfiguration : BaseEntityConfiguration<FileShare>
	{
		public override void ConfigureChild(EntityTypeBuilder<FileShare> builder)
		{
			builder.Property(x => x.UserId).IsRequired();
			builder.Property(x => x.FileId).IsRequired();
			builder.HasOne(x => x.User)
				.WithMany(x => x.FileShares)
				.HasForeignKey(x => x.UserId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.File)
				.WithMany(x => x.FileShares)
				.HasForeignKey(x => x.UserId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
