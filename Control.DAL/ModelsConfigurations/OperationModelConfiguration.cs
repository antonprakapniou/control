using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class OperationModelConfiguration : IEntityTypeConfiguration<Operation>
	{
		public void Configure(EntityTypeBuilder<Operation> builder)
		{
			builder
				.ToTable("Operations")
				.HasKey(_ => _.OperationId)
				.HasName("OperationId");

			builder
				.HasMany(_ => _.Positions)
				.WithOne(_ => _.Operation)
				.OnDelete(DeleteBehavior.SetNull); ;

			builder
				.Property(_ => _.OperationId)
				.HasColumnName("Id");

			builder
				.Property(_ => _.Name)
				.HasColumnName("Name");
		}
	}
}
