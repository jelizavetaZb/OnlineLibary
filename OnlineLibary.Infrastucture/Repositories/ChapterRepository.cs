﻿using OnlineLibary.Domain.Entities.BookEntities;

namespace OnlineLibary.Infrastructure.Repositories
{
    public class ChapterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChapterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Chapter> GetByBookId(int id)
        {
            return _dbContext.Chapters.Where(x => x.BookId == id);
        }

        public Chapter GetById(int id)
        {
            return _dbContext.Chapters.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Chapter entity)
        {
            _dbContext.Chapters.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Chapter entity)
        {
            _dbContext.Chapters.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteRange(IEnumerable<Chapter> enities)
        {
            _dbContext.Chapters.RemoveRange(enities);
            _dbContext.SaveChanges();
        }

        public int Insert(Chapter entity)
        {
            _dbContext.Chapters.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

    }
}
