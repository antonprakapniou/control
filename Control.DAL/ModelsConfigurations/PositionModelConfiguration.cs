using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class PositionModelConfiguration : IEntityTypeConfiguration<Position>
	{
		public void Configure(EntityTypeBuilder<Position> builder)
		{
			builder.ToTable("Positions")
				.HasKey(_ => _.PositionId)
				.HasName("PositionId");

			#region Alternative one-to-many relationship setup

			//builder
			//	.HasOne(_ => _.Measuring)
			//	.WithMany(_=>_.Positions);

			//builder
			//	.HasOne(_ => _.Nomination)
			//	.WithMany(_ => _.Positions);

			//builder
			//	.HasOne(_ => _.Operation)
			//	.WithMany(_ => _.Positions);

			//builder
			//	.HasOne(_ => _.Owner)
			//	.WithMany(_ => _.Positions);

			//builder
			//	.HasOne(_ => _.Period)
			//	.WithMany(_ => _.Positions);

			//builder
			//	.HasOne(_ => _.Status)
			//	.WithMany(_ => _.Positions);

			//builder
			//	.HasOne(_ => _.Units)
			//	.WithMany(_ => _.Positions);

			#endregion

			builder.Property(_ => _.PositionId)
				.HasColumnName("Id");

			builder.Property(_ => _.Name)
				.HasColumnName("Name")
				.IsRequired();

			builder.Property(_ => _.Minimum)
				.HasColumnName("Minimum");

			builder.Property(_ => _.Maximum)
				.HasColumnName("Maximum");

			builder.Property(_ => _.Accuracy)
				.HasColumnName("Accuracy");

			builder.Property(_ => _.Included)
				.HasColumnName("Included");

			builder.Property(_ => _.Addition)
				.HasColumnName("Addition");

			builder.Property(_ => _.Previous)
				.HasColumnName("Previous");

			builder.Property(_ => _.Next)
				.HasColumnName("Next");

			builder.Property(_ => _.Created)
				.HasColumnName("Created");
		}
	}
}
