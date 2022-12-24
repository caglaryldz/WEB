using System.ComponentModel.DataAnnotations;

namespace WebApplication99.Models
{
    public class LoginViewModel
    {
        //[Display(Name ="Kullanıcı Adı", Prompt ="johndoe")]
        [Required(ErrorMessage = "Kullanici adi bos gecilemez")]
        [StringLength(30, ErrorMessage = "Kullanici adi en fazla 30 karakter olabilir.")]
        public string Username { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Sifre alani bos gecilemez")]
        [MinLength(6, ErrorMessage = "Sifre en az 6 karakterden olusmalidir.")]
        [MaxLength(16, ErrorMessage = "Sifre en fazla 16 karakterden olusmalidir.")]
        public string Password { get; set; }
    }
}
