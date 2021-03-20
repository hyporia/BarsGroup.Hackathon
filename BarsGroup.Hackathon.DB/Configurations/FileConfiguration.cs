using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.DB.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abdt.ElectronicArchive.AuthorizationService.Storage.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация <see cref="Privilege"/>
	/// </summary>
	internal class FileConfiguration : BaseEntityConfiguration<File>
	{
		public override void ConfigureChild(EntityTypeBuilder<File> builder)
		{
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.Size).IsRequired();
			builder.Property(x => x.ContentType).IsRequired();
			builder.Property(x => x.Address).IsRequired();
			builder.Property(x => x.UserId).IsRequired();
			builder.HasOne(x => x.User)
				.WithMany(x => x.Files)
				.HasForeignKey(x => x.UserId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
