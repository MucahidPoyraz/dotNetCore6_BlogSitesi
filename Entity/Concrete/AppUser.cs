using Microsoft.AspNetCore.Identity;

namespace Entity.Concrete
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid? ImageId { get; set; } // ImageId nullable olmalı
        public Image? Image { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
