using Comfy.Product.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comfy.Db.SQL.Mappers
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(prop => prop.Id);

            builder
                .Property(prop => prop.Deleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .Property(prop => prop.IdentityReference)
                .HasColumnType("VARCHAR")
                .HasMaxLength(36)
                .IsRequired();

            builder
                .Property(prop => prop.GivenName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(16)
                .IsRequired();

            builder
                .Property(prop => prop.SurName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(16)
                .IsRequired();

            builder
                .Property(prop => prop.Email)
                .HasColumnType("VARCHAR")
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
