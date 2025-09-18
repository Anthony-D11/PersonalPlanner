using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class PersonalPlannerContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoTag>()
            .HasKey(k => new { k.TodoId, k.TagId });

            modelBuilder.Entity<TodoTag>()
            .HasOne(k => k.Todo)
            .WithMany(k => k.TodosTags)
            .HasForeignKey(k => k.TodoId);

            modelBuilder.Entity<TodoTag>()
            .HasOne(k => k.Tag)
            .WithMany(k => k.TodosTags)
            .HasForeignKey(k => k.TagId);



        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TodoTag> TodosTags { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}