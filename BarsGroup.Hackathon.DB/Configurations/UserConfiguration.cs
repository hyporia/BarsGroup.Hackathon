using BarsGroup.Hackathon.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarsGroup.Hackathon.DB.Configurations
{
	/// <summary>
	/// Базовая конфигурация для базовой сущности
	/// </summary>
	internal abstract class UserConfiguration : IEntityTypeConfiguration<User>
	{
		private const string GuidCommand = "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)";
		public void Configure(EntityTypeBuilder<User> builder)
		{
			ConfigureBase(builder);
			ConfigureChild(builder);
		}

		public virtual void ConfigureBase(EntityTypeBuilder<User> builder)
		{
			builder.Property(x => x.Id).IsRequired()
				.HasDefaultValueSql(GuidCommand);

			builder.HasMany(x => x.Files)
				.WithOne(x => x.User)
				.HasForeignKey(x => x.UserId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.Restrict);
		}

		public abstract void ConfigureChild(EntityTypeBuilder<User> builder);
	}
}
