using Microsoft.EntityFrameworkCore;
using OnlineLibary.Domain.Entities;

namespace OnlineLibary.Infrastucture
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<TestItem> TestItems { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}