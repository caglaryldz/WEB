using System.ComponentModel.DataAnnotations;

namespace WebApplication99.Models
{
    public class MovieModel
    {
        public Guid Id { get; set; }
        public string? MovieName { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? MovieImageFileName { get; set; } = "no-image.jpg";
        public string Role { get; set; } = "user";
    }
}