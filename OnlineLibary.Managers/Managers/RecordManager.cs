using AutoMapper;
using OnlineLibary.Domain.Entities;
using OnlineLibary.Infrastructure.Repositories;

namespace OnlineLibary.Managers.Managers
{
    public class RecordManager : BaseManager
    {
        private readonly UserBookRepository _userBookRepository;

        public RecordManager(IMapper mapper, UserBookRepository userBookRepository) : base(mapper)
        {
            _userBookRepository = userBookRepository;
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
