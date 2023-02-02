namespace Control.DAL.ModelsConfigurations;

public sealed class PeriodModelConfiguration : IEntityTypeConfiguration<Period>
{
    public void Configure(EntityTypeBuilder<Period> builder)
    {
        builder
            .ToTable("Periods")
            .HasKey(_ => _.Id)
            .HasName("Id");

        builder
            .HasMany(_ => _.Positions)
            .WithOne(_ => _.Period)
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