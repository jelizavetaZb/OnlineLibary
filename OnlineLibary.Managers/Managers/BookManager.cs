using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Configuration;
using OnlineLibary.Domain.Entities.BookEntities;
using OnlineLibary.Infrastructure.Repositories;
using OnlineLibary.Managers.Models;

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

        public List<BookTableModel> GetBookGridModels(int? userId)
        {
            var query = _bookRepository.GetAll();
            if (userId.HasValue)
            {
                var userBooks = _userBookRepository.GetAll().Where(x => x.UserId == userId).Select(x => x.BookId).AsEnumerable();
                query = query.Where(x => userBooks.Contains(x.Id));
            }
            query.OrderByDescending(x => x.DateUpdated);
            return query.ProjectTo<BookTableModel>(MapperConfig).ToList();
        }

        public void DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);
            var records = _userBookRepository.GetAll().Where(x => x.BookId == book.Id).AsEnumerable();
            _userBookRepository.DeleteRange(records);
            var chapters = _chapterRepository.GetByBookId(book.Id);
            _chapterRepository.DeleteRange(chapters);
            _bookRepository.Delete(book);
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

    }
}