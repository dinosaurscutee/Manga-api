using System.ComponentModel.DataAnnotations;

namespace DinoManga.Models
{
    public class Chapter
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Title { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public string PdfUrl { get; set; }

        [Required]
        public int MangaId { get; set; }

        public Manga Manga { get; set; }

    }

}
