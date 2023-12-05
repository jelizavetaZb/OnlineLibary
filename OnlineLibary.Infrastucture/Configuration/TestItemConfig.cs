using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities;

namespace OnlineLibary.Infrastucture.Configuration
{
    public class TestItemConfig : IEntityTypeConfiguration<TestItem>
    {
        public void Configure(EntityTypeBuilder<TestItem> builder)
        {
        }
    }
}
