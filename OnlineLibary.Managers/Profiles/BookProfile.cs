using AutoMapper;
using OnlineLibary.Domain.Entities.BookEntities;
using OnlineLibary.Managers.Models;

namespace OnlineLibary.Managers.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookTableModel>()
                .ForMember(dest => dest.ChapterCount, opt => opt.MapFrom(scr => scr.Chapters == null ? 0 : scr.Chapters.Count()));
            CreateMap<Book, BookEditInputModel>();
            CreateMap<Chapter, ChapterTableModel>();
        }
    }
}
