using OnlineLibary.Domain.Entities.BookEntities;
using OnlineLibary.Domain.Entities.UserEntities;

namespace OnlineLibary.Domain.Entities
{
    public class UserBook
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int ChapterId { get; set; }
        public virtual Chapter Chapter { get; set; }

    }
}
