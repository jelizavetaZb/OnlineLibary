using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities;

namespace OnlineLibary.Infrastructure.Configuration
{
    public class UserBookConfig : IEntityTypeConfiguration<UserBook>
    {
        public void Configure(EntityTypeBuilder<UserBook> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.Records).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Book).WithMany(x => x.Records).HasForeignKey(x => x.BookId);
            builder.HasOne(x => x.Chapter).WithMany(x => x.Records).HasForeignKey(x => x.ChapterId);
            builder.HasKey(x => new { x.BookId, x.UserId });
        }
    }
}
