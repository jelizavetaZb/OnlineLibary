using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Configuration;
using OnlineLibary.Domain.Entities.BookEntities;
using OnlineLibary.Infrastructure.Repositories;
using OnlineLibary.Managers.Helpers;
using OnlineLibary.Managers.Models;

namespace OnlineLibary.Managers.Managers
{
    public class ChapterManager : BaseManager
    {
        private readonly IConfiguration _config;
        private readonly UserBookRepository _userBookRepository;
        private readonly FileHelper _fileManager;
        private readonly ChapterRepository _chapterRepository;

        public ChapterManager(IConfiguration config, IMapper mapper, FileHelper fileManager,
            ChapterRepository chapterRepository, UserBookRepository userBookRepository) : base(mapper)
        {
            _config = config;
            _fileManager = fileManager;
            _chapterRepository = chapterRepository;
            _userBookRepository = userBookRepository;
        }

        public void DeleteChapter(int id)
        {
            var chapter = _chapterRepository.GetById(id);
            var records = _userBookRepository.GetAll().Where(x => x.ChapterId == chapter.Id);
            _userBookRepository.DeleteRange(records);
            _chapterRepository.Delete(chapter);
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

    }
}
