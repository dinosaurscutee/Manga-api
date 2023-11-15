using System.ComponentModel.DataAnnotations;

namespace DinoManga.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public List<Manga> Mangas { get; set; }

    }
}
