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

        public UserRole GetById(int id)
        {
            return _dbContext.UserRoles.FirstOrDefault(x => x.Id == id);
        }

        public void Update(UserRole role)
        {
            _dbContext.UserRoles.Update(role);
            _dbContext.SaveChanges();
        }

        public IQueryable<UserRole> GetAll()
        {
            return _dbContext.UserRoles;
        }
    }
}
