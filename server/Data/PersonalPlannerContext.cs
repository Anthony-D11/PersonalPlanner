using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class PersonalPlannerContext : DbContext
    {
        public PersonalPlannerContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Activity>().HasData(
                new Activity
                {
                    id = 1,
                    content = "Go shopping",
                    details = "",
                    completed = false
                },
                new Activity
                {
                    id = 2,
                    content = "Go camping",
                    details = "Go with my family",
                    completed = false
                }
            );

        }

        public DbSet<Activity> activities { get; set; }
    }
}