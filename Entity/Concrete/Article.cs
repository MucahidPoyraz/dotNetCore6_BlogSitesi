namespace Entity.Concrete
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; } = 0;

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid? ImageId { get; set; }
        public Image? Image { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }

}
