using Microsoft.EntityFrameworkCore;

namespace OnlineLibary.Infrastucture
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=OnlineLibary;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}