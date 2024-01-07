using OnlineLibary.Domain.Entities.UserEntities;

namespace OnlineLibary.Infrastructure.Repositories
{
    public class UserUserRolesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserUserRolesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(IEnumerable<UserUserRole> enities)
        {
            _dbContext.UserUserRoles.AddRange(enities);
            _dbContext.SaveChanges();
        }

        public void DeleteRange(IEnumerable<UserUserRole> enities)
        {
            _dbContext.UserUserRoles.RemoveRange(enities);
            _dbContext.SaveChanges();
        }
    }
}
