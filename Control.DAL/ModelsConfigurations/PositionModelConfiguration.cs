﻿using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
	public sealed class PositionModelConfiguration : IEntityTypeConfiguration<Position>
	{
		public void Configure(EntityTypeBuilder<Position> builder)
		{
			builder
				.ToTable("Positions")
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
            //	
            //builder
            //	.HasOne(_ => _.Category)
            //	.WithMany(_ => _.Categories);				

            #endregion

            builder
                .Property(_ => _.PositionId)
				.HasColumnName("Id");

			builder
				.Property(_ => _.Name)
				.HasColumnName("Name")
				.IsRequired();

			#region You never know

			//builder.Property(_ => _.Minimum)
			//	.HasColumnName("Minimum");

			//builder.Property(_ => _.Maximum)
			//	.HasColumnName("Maximum");

			//builder.Property(_ => _.Accuracy)
			//	.HasColumnName("Accuracy");

			#endregion

			builder
				.Property(_ => _.Description)
				.HasColumnName("Description");

			builder
				.Property(_ => _.Included)
				.HasColumnName("Included");

			builder
				.Property(_ => _.Addition)
				.HasColumnName("Addition");

			builder
				.Property(_ => _.PreviousDate)
				.HasColumnName("PreviousDate");

			builder
				.Property(_ => _.NextDate)
				.HasColumnName("NextDate");

            builder
                .Property(_ => _.Status)
                .HasColumnName("Status");

            builder
				.Property(_ => _.Created)
				.HasColumnName("Created");
		}
	}
}
