namespace Control.DAL.ModelsConfigurations;

public sealed class NominationModelConfiguration : IEntityTypeConfiguration<Nomination>
{
    public void Configure(EntityTypeBuilder<Nomination> builder)
    {
        builder
            .ToTable("Nominations")
            .HasKey(_ => _.Id)
            .HasName("Id");

        builder
            .HasMany(_ => _.Positions)
            .WithOne(_ => _.Nomination)
            .OnDelete(DeleteBehavior.SetNull); ;

        builder
            .Property(_ => _.Id)
            .HasColumnName("Id");

        builder
            .Property(_ => _.Name)
            .HasColumnName("Name")
            .IsRequired();
    }
}
