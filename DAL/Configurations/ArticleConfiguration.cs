using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(a => a.Title)
                .HasMaxLength(150)
                .IsRequired();

            // Article -> Category İlişkisi
            builder.HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Category silinirse makaleler etkilenmez

            // Article -> Image İlişkisi
            builder.HasOne(a => a.Image)
                .WithMany()
                .HasForeignKey(a => a.ImageId)
                .OnDelete(DeleteBehavior.SetNull); // Article silinirse Image silinmez

            // Article -> AppUser İlişkisi
            builder.HasOne(a => a.AppUser)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.AppUserId)
                .OnDelete(DeleteBehavior.Restrict); // AppUser silinirse makaleler silinmez

        }
    }
}
