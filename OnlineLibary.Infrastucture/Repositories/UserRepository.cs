using OnlineLibary.Domain.Entities.UserEntities;

namespace OnlineLibary.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users;
        }
    }
}
