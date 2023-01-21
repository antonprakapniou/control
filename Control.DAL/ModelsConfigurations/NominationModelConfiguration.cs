using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class NominationModelConfiguration : IEntityTypeConfiguration<Nomination>
	{
		public void Configure(EntityTypeBuilder<Nomination> builder)
		{
			builder
				.ToTable("Nominations")
				.HasKey(_ => _.NominationId)
				.HasName("NominationId");

			builder
				.HasMany(_ => _.Positions)
				.WithOne(_ => _.Nomination)
				.OnDelete(DeleteBehavior.SetNull); ;

			builder
				.Property(_ => _.NominationId)
				.HasColumnName("Id");

			builder
				.Property(_ => _.Name)
				.HasColumnName("Name");
		}
	}
}
