using System.ComponentModel.DataAnnotations;

namespace WebApplication99.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string? MovieName { get; set; } = null;
        public string? Yonetmen { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? MovieImageFileName { get; set; } = "no-image.jpg";
        public int CategoryId { get; set; }
    }
    public class CreateMovieModel
    {

        [Required(ErrorMessage = "Film adı alınmıs.")]
        [StringLength(30, ErrorMessage = "Kullanici adi en fazla 30 karakter olabilir.")]
        public string? MovieName { get; set; } = null;

        public string? Yonetmen { get; set; }
        public string? MovieImageFileName { get; set; } = "no-image.jpg";

        public int CategoryId { get; set; }

        public string? Done { get; set; }
    }
}