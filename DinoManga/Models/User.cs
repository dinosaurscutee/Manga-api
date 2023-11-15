using System.ComponentModel.DataAnnotations;

namespace DinoManga.Models

{

    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        public string Token { get; set; }

        public List<Comment> Comments { get; set; }

    }

}