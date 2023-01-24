using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.DAL.ModelsConfigurations
{
    public sealed class CategoryModelConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .ToTable("Categories")
                .HasKey(_ => _.Id)
                .HasName("Id");

            builder
                .HasMany(_ => _.Positions)
                .WithOne(_ => _.Category)
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
}
