namespace Control.DAL.ModelsConfigurations;

public sealed class OperationModelConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder
            .ToTable("Operations")
            .HasKey(_ => _.Id)
            .HasName("Id");

        builder
            .HasMany(_ => _.Positions)
            .WithOne(_ => _.Operation)
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