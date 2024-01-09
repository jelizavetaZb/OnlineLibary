using OnlineLibary.Domain.Entities.UserEntities;

namespace OnlineLibary.Infrastructure.Repositories
{
    public class RolesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RolesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<UserRole> GetAll()
        {
            return _dbContext.UserRoles;
        }
    }
}
