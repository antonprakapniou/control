using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class MeasuringModelConfiguration : IEntityTypeConfiguration<Measuring>
	{
		public void Configure(EntityTypeBuilder<Measuring> builder)
		{
			builder
				.ToTable("Measurings")
				.HasKey(_ => _.Id)
				.HasName("Id");

			builder
				.HasMany(_ => _.Positions)
				.WithOne(_ => _.Measuring)
				.OnDelete(DeleteBehavior.SetNull); ;

			builder
				.Property(_ => _.Id)
				.HasColumnName("Id");

			builder
				.Property(_ => _.Name)
				.HasColumnName("Name")
				.IsRequired();

			builder
				.Property(_ => _.Code)
				.HasColumnName("Code");
		}
	}
}
