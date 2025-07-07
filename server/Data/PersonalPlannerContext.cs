using Microsoft.EntityFrameworkCore;

namespace server.Data
{
    public class PersonalPlannerContext : DbContext
    {
        public PersonalPlannerContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
    }
}