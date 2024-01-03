using OnlineLibary.Domain.Entities.BookEntities;

namespace OnlineLibary.Infrastructure.Repositories
{
    public class BookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Book GetById(int id)
        {
            return _dbContext.Books.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Book entity)
        {
            _dbContext.Books.Update(entity);
            _dbContext.SaveChanges();
        }

        public void AddRange(IEnumerable<Book> enities)
        {
            _dbContext.Books.AddRange(enities);
            _dbContext.SaveChanges();
        }

        public void DeleteRange(IEnumerable<Book> enities)
        {
            _dbContext.Books.RemoveRange(enities);
            _dbContext.SaveChanges();
        }

        public void Delete(Book entity)
        {
            _dbContext.Books.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<Book> GetAll()
        {
            return _dbContext.Books;
        }

        public int Insert(Book entity)
        {
            _dbContext.Books.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

    }
}
