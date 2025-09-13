using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class PersonalPlannerContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoTag> TodoTags { get; set; }
    }
}