using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class PeriodModelConfiguration : IEntityTypeConfiguration<Period>
	{
		public void Configure(EntityTypeBuilder<Period> builder)
		{
			builder.ToTable("Periods")
				.HasKey(_ => _.PeriodId)
				.HasName("PeriodId");

			builder
				.HasMany(_ => _.Positions)
				.WithOne(_ => _.Period)
				.OnDelete(DeleteBehavior.SetNull); ;

			builder.Property(_ => _.PeriodId)
				.HasColumnName("Id");

			builder.Property(_ => _.Name)
				.HasColumnName("Name")
				.IsRequired();

			builder.Property(_ => _.Month)
				.HasColumnName("Month")
				.IsRequired();
		}
	}
}
