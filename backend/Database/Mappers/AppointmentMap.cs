using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Mappers;

public class AppointmentMap : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Deleted).IsRequired();
        builder.Property(prop => prop.Date).HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(prop => prop.ProcedurePerformed).IsRequired();
    }
}