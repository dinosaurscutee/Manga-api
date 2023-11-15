using DinoManga.Models;
using Microsoft.EntityFrameworkCore;

namespace DinoManga.Data
{
    public class MangaDbContext : DbContext
    {

        public MangaDbContext(DbContextOptions options) : base(options)
        {
        }

        public MangaDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manga>()
            .HasOne(m => m.Category)
            .WithMany(c => c.Mangas)
            .HasForeignKey(m => m.CategoryId);

            modelBuilder.Entity<Manga>()
              .HasOne(m => m.Author)
              .WithMany(a => a.Mangas)
              .HasForeignKey(m => m.AuthorId);

            modelBuilder.Entity<Chapter>()
              .HasOne(c => c.Manga)
              .WithMany(m => m.Chapters)
              .HasForeignKey(c => c.MangaId);

            modelBuilder.Entity<Comment>()
              .HasOne(c => c.User)
              .WithMany(u => u.Comments)
              .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
              .HasOne(c => c.Manga)
              .WithMany(m => m.Comments)
              .HasForeignKey(c => c.MangaId);


            SeedCategories(modelBuilder);
            SeedAuthors(modelBuilder);
            SeedUsers(modelBuilder);
            SeedMangas(modelBuilder);
            SeedChapters(modelBuilder);
            SeedComments(modelBuilder);
        }
        private void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Adventure" }
                // Thêm các category khác nếu cần
            );
        }

        private void SeedAuthors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Author 1" },
                new Author { Id = 2, Name = "Author 2" }
                // Thêm các author khác nếu cần
            );
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "User1", Password = "Password1", Role = "User", Token= "" }
                // Thêm các user khác nếu cần
            );
        }

        private void SeedMangas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manga>().HasData(
                new Manga { Id = 1, Title = "Manga 1", AuthorId = 1, CategoryId = 1, Description = "Description 1", ThumbnailImage = "thumbnail1.jpg" },
                new Manga { Id = 2, Title = "Manga 2", AuthorId = 2, CategoryId = 2, Description = "Description 2", ThumbnailImage = "thumbnail2.jpg" }
            // Thêm các manga khác nếu cần
            );
        }

        private void SeedChapters(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chapter>().HasData(
                new Chapter { Id = 1, Title = "Chapter 1", DisplayOrder = 1, PdfUrl = "url1", MangaId = 1 },
                new Chapter { Id = 2, Title = "Chapter 2", DisplayOrder = 2, PdfUrl = "url2", MangaId = 2 }
                // Thêm các chapter khác nếu cần
            );
        }

        private void SeedComments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, Content = "Comment 1", DateCreated = DateTime.Now, UserId = 1, MangaId = 1 }
                // Thêm các comment khác nếu cần
            );
        }
    }

}