using DinoManga.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DinoManga.DTO
{
    public class MangaDTO
    {
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

        public List<string> CategoryNames { get; set; } // Change from List<Category> to List<string>

        public string AuthorName { get; set; }
    }



}
