using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities.BookEntities;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure.Configuration
{
    public class ChapterConfig : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.Property(e => e.DateCreated).DatabaseGenerated();
            builder.HasOne(x => x.Book)
                .WithMany(x => x.Chapters).
                HasForeignKey(x => x.BookId);
        }
    }
}
