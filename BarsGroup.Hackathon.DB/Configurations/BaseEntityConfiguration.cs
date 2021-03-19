using BarsGroup.Hackathon.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarsGroup.Hackathon.DB.Configurations
{
	/// <summary>
	/// Базовая конфигурация для базовой сущности
	/// </summary>
	internal abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
		 where TEntity : BaseEntity
	{
		private const string GuidCommand = "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)";
		private const string NowCommand = "now() at time zone 'utc'";

		public void Configure(EntityTypeBuilder<TEntity> builder)
		{
			ConfigureBase(builder);
			ConfigureChild(builder);
		}

		public virtual void ConfigureBase(EntityTypeBuilder<TEntity> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).IsRequired()
				.HasDefaultValueSql(GuidCommand);

			builder.Property(x => x.CreatedTimestamp)
				.IsRequired()
				.HasDefaultValueSql(NowCommand);

			builder.Property(x => x.ModifyTimestamp)
				.IsRequired();
		}

		public abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
	}
}
