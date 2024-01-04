using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Configuration;
using OnlineLibary.Domain.Entities;
using OnlineLibary.Domain.Entities.BookEntities;
using OnlineLibary.Infrastructure.Repositories;
using OnlineLibary.Managers.Models;
using System.Net;

namespace OnlineLibary.Managers.Managers
{
    public class BookManager : BaseManager
    {
        private readonly IConfiguration _config;
        private readonly BookRepository _bookRepository;
        private readonly UserBookRepository _userBookRepository;
        private readonly FileManager _fileManager;
        private readonly ChapterRepository _chapterRepository;

        public BookManager(IConfiguration config, IMapper mapper, BookRepository bookRepository, FileManager fileManager,
            ChapterRepository chapterRepository, UserBookRepository userBookRepository) : base(mapper)
        {
            _config = config;
            _bookRepository = bookRepository;
            _fileManager = fileManager;
            _chapterRepository = chapterRepository;
            _userBookRepository = userBookRepository;
        }

        public List<BookTableModel> GetBookGridModels()
        {
            var query = _bookRepository.GetAll().OrderByDescending(x => x.DateUpdated);
            return query.ProjectTo<BookTableModel>(MapperConfig).ToList();
        }

        public void DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);
            _bookRepository.Delete(book);
        }

        public void DeleteChapter(int id)
        {
            var chapter = _chapterRepository.GetById(id);
            _chapterRepository.Delete(chapter);
        }

        public BookEditInputModel GetBookEditInputModel(int? id, int? userId)
        {
            if (id == null)
            {
                return null;
            }
            var enity = _bookRepository.GetById(id.Value);
            if (enity == null)
            {
                return null;
            }
            var model = Mapper.Map<BookEditInputModel>(enity);
            model.CurrentChapterId = userId != null ? (_userBookRepository.GetById(userId.Value, id.Value))?.ChapterId : null;
            return model;
        }

        public async Task<ResponseResult> InsertOrUpdateBookAsync(BookEditInputModel model)
        {
            var result = new ResponseResult();
            if (model == null)
            {
                result.AddError(string.Empty, "Form data are empty");
                return result;
            }

            var book = new Book();
            var isNew = true;
            if (model.Id.HasValue)
            {
                isNew = false;
                book = _bookRepository.GetById(model.Id.Value);
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.Author = model.Author;
            book.Year = model.Year;

            if (model.NewCover != null && model.NewCover.Length > 0)
            {
                if (!model.NewCover.ContentType.Contains("image"))
                {
                    result.AddError(nameof(model.NewCover), "Allowed only images");
                    return result;
                }
                var folder = _config["System:CoverImagesFolder"];
                book.CoverUrl = await _fileManager.UploadFile(model.NewCover, folder);
            }

            if (isNew)
            {
                result.UpdatedId = _bookRepository.Insert(book);
                result.IsSuccess = true;
                return result;
            }

            book.DateUpdated = DateTime.Now;
            _bookRepository.Update(book);
            result.UpdatedId = book.Id;
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResponseResult> InsertOrUpdateChapterAsync(ChapterEditInputModel model)
        {
            var result = new ResponseResult();
            if (model == null)
            {
                result.AddError(string.Empty, "Form data are empty");
                return result;
            }

            var chapter = new Chapter();
            var isNew = true;
            if (model.Id.HasValue)
            {
                isNew = false;
                chapter = _chapterRepository.GetById(model.Id.Value);
            }

            chapter.Title = model.Title;
            chapter.Content = model.Content;
            chapter.BookId = model.BookId;
            chapter.Number = model.Number;

            if (model.NewImage != null && model.NewImage.Length > 0)
            {
                if (!model.NewImage.ContentType.Contains("image"))
                {
                    result.AddError(nameof(model.NewImage), "Allowed only images");
                    return result;
                }
                var folder = _config["System:ChapterImagesFolder"];
                chapter.ImageUrl = await _fileManager.UploadFile(model.NewImage, folder);
            }

            if (isNew)
            {
                result.UpdatedId = _chapterRepository.Insert(chapter);
                result.IsSuccess = true;
                return result;
            }

            chapter.DateUpdated = DateTime.Now;
            _chapterRepository.Update(chapter);
            result.UpdatedId = chapter.Id;
            result.IsSuccess = true;
            return result;
        }

        public List<ChapterTableModel> GetChapterTable(int? bookId)
        {
            if (bookId == null) return null;
            var query = _chapterRepository.GetByBookId(bookId.Value).OrderBy(x => x.Number);
            return query.ProjectTo<ChapterTableModel>(MapperConfig).ToList();
        }

        public ChapterEditInputModel GetChapterEditInputModel(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var enity = _chapterRepository.GetById(id.Value);
            if (enity == null)
            {
                return null;
            }
            var model = Mapper.Map<ChapterEditInputModel>(enity);
            model.PreviousId = GetPreviousChapterId(id.Value, enity.BookId);
            model.NextId = GetNextChapterId(id.Value, enity.BookId);

            return model;
        }

        private int? GetNextChapterId(int chapterId, int bookId)
        {
            var chapters = _chapterRepository.GetByBookId(bookId).OrderBy(x => x.Number).ToList();
            var current = chapters.Where(x => x.Id == chapterId).FirstOrDefault();
            var index = chapters.IndexOf(current);
            if (current == null || chapters.Count < 2 || index == chapters.Count - 1)
            {
                return null;
            }
            var next = chapters[index + 1];
            return next?.Id;
        }

        private int? GetPreviousChapterId(int chapterId, int bookId)
        {
            var chapters = _chapterRepository.GetByBookId(bookId).OrderBy(x => x.Number).ToList();
            var current = chapters.Where(x => x.Id == chapterId).FirstOrDefault();
            var index = chapters.IndexOf(current);
            if (current == null || chapters.Count < 2 || index < 1)
            {
                return null;
            }
            var previous = chapters[index - 1];
            return previous?.Id;
        }

        public void UpdateRecord(int bookId, int chapterId, int userId)
        {
            var oldRecord = _userBookRepository.GetById(userId, bookId);
            if (oldRecord != null)
            {
                _userBookRepository.Delete(oldRecord);
            }
            var record = new UserBook { BookId = bookId, ChapterId = chapterId, UserId = userId };
            _userBookRepository.Insert(record);
        }
    }
}