using BarsGroup.Hackathon.Core.Entities;
using BarsGroup.Hackathon.DB.Configurations;
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
			//builder.Property(x => x.RoleId).IsRequired();
			//builder.Property(x => x.PrivilegeResource).IsRequired();
			//builder.Property(x => x.PrivilegeLevel).IsRequired();
			//builder.HasOne(x => x.Role)
			//	.WithMany(x => x.Privileges)
			//	.HasForeignKey(x => x.RoleId)
			//	.HasPrincipalKey(x => x.Id)
			//	.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
