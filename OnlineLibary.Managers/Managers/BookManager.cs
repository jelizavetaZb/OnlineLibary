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
        private readonly FileManager _fileManager;
        private readonly ChapterRepository _chapterRepository;

        public BookManager(IConfiguration config, IMapper mapper, BookRepository bookRepository, FileManager fileManager, 
            ChapterRepository chapterRepository) : base(mapper)
        {
            _config = config;
            _bookRepository = bookRepository;
            _fileManager = fileManager;
            _chapterRepository = chapterRepository;
        }

        public List<BookTableModel> GetBookGridModels()
        {
            var query = _bookRepository.GetAll().OrderByDescending(x => x.DateUpdated);
            return query.ProjectTo<BookTableModel>(MapperConfig).ToList();
        }

        public BookEditInputModel GetBookEditInputModel(int? id)
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
            return Mapper.Map<BookEditInputModel>(enity);
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

        public List<ChapterTableModel> GetChapterTable(int? bookId)
        {
            if (bookId == null) return null;
            var query = _chapterRepository.GetByBookId(bookId.Value).OrderByDescending(x => x.Number);
            return query.ProjectTo<ChapterTableModel>(MapperConfig).ToList();
        }
    }
}

