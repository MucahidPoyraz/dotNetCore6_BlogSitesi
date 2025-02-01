using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(i => i.FileName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(i => i.FileType)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
