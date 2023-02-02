namespace Control.DAL.ModelsConfigurations;

public sealed class OwnerModelConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder
            .ToTable("Owners")
            .HasKey(_ => _.Id)
            .HasName("Id");

        #region Alternative one-to-many relationship setup

        //builder
        //    .HasOne(_ => _.Master)
        //    .WithMany(_ => _.Owners);

        #endregion

        builder
            .HasMany(_ => _.Positions)
            .WithOne(_ => _.Owner)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .Property(_ => _.Id)
            .HasColumnName("Id");

        builder
            .Property(_ => _.FullShop)
            .HasColumnName("FullShop");

        builder
            .Property(_ => _.ShortShop)
            .HasColumnName("ShortShop")
            .IsRequired();

        builder
            .Property(_ => _.FullProduction)
            .HasColumnName("FullProduction");

        builder
            .Property(_ => _.ShortProduction)
            .HasColumnName("ShortProduction");

        builder
            .Property(_ => _.ShopCode)
            .HasColumnName("ShopCode");

        builder
            .Property(_ => _.Phone)
            .HasColumnName("Phone");
    }
}