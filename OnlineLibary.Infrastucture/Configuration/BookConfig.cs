using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities.BookEntities;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure.Configuration
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(e => e.DateCreated).DatabaseGenerated();
            builder.Property(e => e.DateUpdated).DatabaseGenerated();
        }
    }
}
