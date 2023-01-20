using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class MeasuringModelConfiguration : IEntityTypeConfiguration<Measuring>
	{
		public void Configure(EntityTypeBuilder<Measuring> builder)
		{
			builder.ToTable("Measurings")
				.HasKey(_ => _.MeasuringId)
				.HasName("MeasuringId");

			builder
				.HasMany(_ => _.Positions)
				.WithOne(_ => _.Measuring)
				.OnDelete(DeleteBehavior.SetNull); ;

			builder.Property(_ => _.MeasuringId)
				.HasColumnName("Id");

			builder.Property(_ => _.Name)
				.HasColumnName("Name")
				.IsRequired();

			builder.Property(_ => _.Code)
				.HasColumnName("Code")
				.IsRequired();
		}
	}
}
