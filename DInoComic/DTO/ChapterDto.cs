namespace DInoComic.DTO
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public int ChapterNumber { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public int Status { get; set; }
        public bool IsActive { get; set; }
        public string FilePdf { get; set; }

        public int Page { get; set; }
        public string MangaName { get; set; }


    }
}
