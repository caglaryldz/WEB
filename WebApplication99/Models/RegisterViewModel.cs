using System.ComponentModel.DataAnnotations;

namespace WebApplication99.Models
{
    public class RegisterViewModel
    {
        //[Display(Name ="Kullanıcı Adı", Prompt ="johndoe")]
        [Required(ErrorMessage = "Kullanici adi bos gecilemez")]
        [StringLength(30, ErrorMessage = "Kullanici adi en fazla 30 karakterden olusmalidir")]
        public string Username { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Sifre alani bos gecilemez")]
        [MinLength(6, ErrorMessage = "Sifre en az 6 karakterden olusmalıdır.")]
        [MaxLength(16, ErrorMessage = "Sifre en fazla 16 karakterden olusmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "İkinci sifre alani bos gecilemez")]
        [MinLength(6, ErrorMessage = "İkincil Sifre en az 6 karakterden olusmalıdır.")]
        [MaxLength(16, ErrorMessage = "İkincil Sifre en fazla 16 karakterden olusmalıdır.")]
        [Compare(nameof(Password), ErrorMessage ="Sifre ile ikincil sifre eslesmiyor.")]
        public string RePassword { get; set; }
    }
}
