using System.ComponentModel.DataAnnotations;

namespace DinoManga.Models
{
    public class Manga
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int ViewCount { get; set; }

        [StringLength(1000)]
        public string ThumbnailImage { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Category Category { get; set; }

        public Author Author { get; set; }

        public List<Chapter> Chapters { get; set; }

        public List<Comment> Comments { get; set; }

    }

}
