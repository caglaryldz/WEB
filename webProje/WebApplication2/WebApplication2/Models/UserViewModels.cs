
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? FullName { get; set; } = null;

        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Role { get; set; } = "user";

    }
}
