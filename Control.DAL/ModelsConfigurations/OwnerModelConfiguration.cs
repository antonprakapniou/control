using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	internal class OwnerModelConfiguration : IEntityTypeConfiguration<Owner>
	{
		public void Configure(EntityTypeBuilder<Owner> builder)
		{
			builder.ToTable("Owners")
				.HasKey(_ => _.OwnerId)
				.HasName("OwnerId");

			builder
				.HasMany(_ => _.Positions)
				.WithOne(_ => _.Owner)
				.OnDelete(DeleteBehavior.SetNull); ;

			builder.Property(_ => _.OwnerId)
				.HasColumnName("Id");

			builder.Property(_ => _.Shop)
				.HasColumnName("Shop");

			builder.Property(_ => _.Production)
				.HasColumnName("Production");

			builder.Property(_ => _.Master)
				.HasColumnName("Master");

			builder.Property(_ => _.Phone)
				.HasColumnName("Phone");

			builder.Property(_ => _.Email)
				.HasColumnName("Email");
		}
	}
}
