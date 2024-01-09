using OnlineLibary.Domain.Entities;
using OnlineLibary.Domain.Entities.BookEntities;

namespace OnlineLibary.Infrastructure.Repositories
{
    public class UserBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserBookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserBook GetById(int userId, int bookId)
        {
            return _dbContext.UserBooks.FirstOrDefault(x => x.UserId == userId && x.BookId == bookId);
        }

        public void DeleteRange(IEnumerable<UserBook> enities)
        {
            _dbContext.UserBooks.RemoveRange(enities);
            _dbContext.SaveChanges();
        }

        public void Delete(UserBook entity)
        {
            _dbContext.UserBooks.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<UserBook> GetAll()
        {
            return _dbContext.UserBooks;
        }

        public UserBook Insert(UserBook entity)
        {
            _dbContext.UserBooks.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
