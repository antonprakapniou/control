using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class StatusModelConfiguration : IEntityTypeConfiguration<Status>
	{
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			builder
				.ToTable("Statuses")
				.HasKey(_ => _.StatusId)
				.HasName("StatusId");

			builder
				.HasMany(_ => _.Positions)
				.WithOne(_ => _.Status)
				.OnDelete(DeleteBehavior.SetNull); ;

			builder
				.Property(_ => _.StatusId)
				.HasColumnName("Id");

			builder
				.Property(_ => _.Name)
				.HasColumnName("Name")
				.IsRequired();
		}
	}
}
