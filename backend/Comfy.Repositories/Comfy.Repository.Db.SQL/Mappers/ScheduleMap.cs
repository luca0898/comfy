using Comfy.Product.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Comfy.Db.SQL.Mappers
{
    public class ScheduleMap : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder
                .HasKey(prop => prop.Id);

            builder
                .Property(prop => prop.Deleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .Property(prop => prop.Date)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .Property(prop => prop.ProcedurePerformed)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
