using DInoComic.Models;
using System.ComponentModel.DataAnnotations;

namespace DInoComic.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        public string? PasswordHash { get; set; }
        public string? FullName { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }

    }
}
