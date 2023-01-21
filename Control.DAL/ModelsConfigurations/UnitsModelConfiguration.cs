using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class UnitsModelConfiguration : IEntityTypeConfiguration<Units>
	{
		public void Configure(EntityTypeBuilder<Units> builder)
		{
			builder
				.ToTable("Units")
				.HasKey(_ => _.UnitsId)
				.HasName("UnitsId");

			builder
				.HasMany(_ => _.Positions)
				.WithOne(_ => _.Units)
				.OnDelete(DeleteBehavior.SetNull);

			builder
				.Property(_ => _.UnitsId)
				.HasColumnName("Id");

			builder
				.Property(_ => _.Name)
				.HasColumnName("Name")
				.IsRequired();
		}
	}
}
