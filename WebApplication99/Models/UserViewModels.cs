using System.ComponentModel.DataAnnotations;

namespace WebApplication99.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; } = null;
        public string Username { get; set; }
        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ProfileImageFileName { get; set; } = "no-image.jpg";
        public string Role { get; set; } = "user";
    }

    public class CreateUserModel
    {
        
        [Required(ErrorMessage = "Kullanıcı adı alınmıs.")]
        [StringLength(30, ErrorMessage = "Kullanici adi en fazla 30 karakter olabilir.")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        public bool Locked { get; set; }

        
        [Required(ErrorMessage = "Sifre alanı bos gecilemez.")]
        [MinLength(6, ErrorMessage = "Sifre en az 6 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Sifre en fazla 16 karakter olabilir.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "İkncil Sifre alanı bos gecilemez.")]
        [MinLength(6, ErrorMessage = "İkincil Sifre en az 6 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "İkincil Sifre en fazla 16 karakter olabilir.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        public string? Done { get; set; }
    }

    public class EditUserModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Kullanci Adi bos gecilemez.")]
        [StringLength(30, ErrorMessage = "Kullanici adi en fazla 30 karakter olabilir.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Kullanci Adi, soyadi bos gecilemez.")]
        [StringLength(50)]
        public string FullName { get; set; }

        public bool Locked { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        public string? Done { get; set; }
    }
}
