namespace Control.DAL.ModelsConfigurations;

public sealed class HistoryModelConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder
            .ToTable("History")
            .HasKey(_ => _.Id)
            .HasName("Id");

        builder
            .Property(_ => _.Id)
            .HasColumnName("Id");

        builder
            .Property(_ => _.Created)
            .HasColumnName("Created")
            .IsRequired();

        builder
            .Property(_ => _.DeviceType)
            .HasColumnName("Device_type")
            .IsRequired();

        builder
            .Property(_ => _.FactoryNumber)
            .HasColumnName("Factory_number")
            .IsRequired();

        builder
            .Property(_ => _.Owner)
            .HasColumnName("Owner")
            .IsRequired();

        builder
            .Property(_ => _.Master)
            .HasColumnName("Master")
            .IsRequired();
    }
}
