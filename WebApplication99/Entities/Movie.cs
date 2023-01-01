using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication99.Entities
{
    [Table("Movies")]
    public class Movie
    {
       //[Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? MovieName { get; set; } = null;
        public string? Yonetmen { get; set; }
        /*public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string? MovieImageFileName { get; set; } = "no-image.jpg";
        public int CategoryId { get; set; }*/


    }
}

