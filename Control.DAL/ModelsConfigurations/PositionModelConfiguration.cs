namespace Control.DAL.ModelsConfigurations;

public sealed class PositionModelConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder
            .ToTable("Positions")
            .HasKey(_ => _.Id)
            .HasName("Id");

        #region Alternative one-to-many relationship setup

        //builder
        //	.HasOne(_ => _.Measuring)
        //	.WithMany(_ => _.Positions);

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
            .Property(_ => _.Id)
            .HasColumnName("Id");

        builder
            .Property(_ => _.DeviceType)
            .HasColumnName("Type")
            .IsRequired();

        builder
            .Property(_ => _.FactoryNumber)
            .HasColumnName("Number")
            .IsRequired();

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
            .HasColumnName("PreviousDate")
            .IsRequired();

        builder
            .Property(_ => _.NextDate)
            .HasColumnName("NextDate")
            .IsRequired();

        builder
            .Property(_ => _.Created)
            .HasColumnName("Created")
            .IsRequired();

        builder
            .Property(_ => _.Picture)
            .HasColumnName("Picture");
    }
}