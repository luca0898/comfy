using Comfy.Db.SQL.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            modelBuilder.Ignore<Claim>();
            modelBuilder.HasDefaultSchema("ComfyDb");

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ScheduleMap());
        }
    }
}
