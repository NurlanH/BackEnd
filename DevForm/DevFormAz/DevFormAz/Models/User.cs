using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Email Daxil olunmayıb və ya bazada belə bir email artıq mövcuddur")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ad Daxil olunmayıb")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad Daxil olunmayıb")]
        public string Lastname  { get; set; }

        [Required(ErrorMessage = "Şifrə Daxil olunmayıb və ya təsdiq şifrəsi ilə uyğun gəlmir")]
        [StringLength(255, ErrorMessage = "Şifrənin uzunluğu 6`dan böyük olmalıdır", MinimumLength = 6)]
        public string Password { get; set; }

        public Guid UserControlPoint { get; set; } = Guid.NewGuid();

        public bool IsActive { get; set; } = false;

        public virtual ICollection<FormLike> FormLikes { get; set; }
        public virtual ICollection<FormViewCount> FormViews { get; set; }

    }
}