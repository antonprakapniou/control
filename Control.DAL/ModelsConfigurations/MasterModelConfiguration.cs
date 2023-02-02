namespace Control.DAL.ModelsConfigurations;

public sealed class MasterModelConfiguration : IEntityTypeConfiguration<Master>
{
    public void Configure(EntityTypeBuilder<Master> builder)
    {
        builder
            .ToTable("Masters")
            .HasKey(_ => _.Id)
            .HasName("Id");

        builder
            .HasMany(_ => _.Owners)
            .WithOne(_ => _.Master)
            .OnDelete(DeleteBehavior.SetNull); ;

        builder
            .Property(_ => _.Id)
            .HasColumnName("Id");

        builder
            .Property(_ => _.Name)
            .HasColumnName("Name")
            .IsRequired();

        builder
            .Property(_ => _.Phone)
            .HasColumnName("Phone");

        builder
            .Property(_ => _.Email)
            .HasColumnName("Email");
    }
}