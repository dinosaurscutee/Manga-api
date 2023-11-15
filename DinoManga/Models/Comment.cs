using System.ComponentModel.DataAnnotations;

namespace DinoManga.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MangaId { get; set; }

        public Manga Manga { get; set; }
        public User User { get; set; }
    }
}
