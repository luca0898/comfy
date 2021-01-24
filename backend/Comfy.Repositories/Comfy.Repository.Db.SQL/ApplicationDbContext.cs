using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Comfy.Db.SQL.Mappers;
using Comfy.PRODUCT.Entities;

namespace Comfy.Repository.Db.SQL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext Context => this;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ComfyDb");

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ScheduleMap());
        }
    }
}
