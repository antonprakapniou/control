namespace Control.DAL.ModelsConfigurations;

public sealed class MasterModelConfiguration:IEntityTypeConfiguration<Master>
{
    public void Configure(EntityTypeBuilder<Master> builder)
    {
        builder
            .HasMany(_ => _.Owners)
            .WithOne(_ => _.Master)
            .HasForeignKey(_ => _.MasterId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .Property(_ => _.Name)
            .HasColumnName("Name")
            .IsRequired();
    }
}
