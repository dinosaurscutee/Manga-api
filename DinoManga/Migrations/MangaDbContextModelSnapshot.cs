﻿// <auto-generated />
using System;
using DinoManga.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DinoManga.Migrations
{
    [DbContext(typeof(MangaDbContext))]
    partial class MangaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DinoManga.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Author 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Author 2"
                        });
                });

            modelBuilder.Entity("DinoManga.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Adventure"
                        });
                });

            modelBuilder.Entity("DinoManga.Models.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<int>("MangaId")
                        .HasColumnType("int");

                    b.Property<string>("PdfUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("MangaId");

                    b.ToTable("Chapters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            MangaId = 1,
                            PdfUrl = "url1",
                            Title = "Chapter 1"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            MangaId = 2,
                            PdfUrl = "url2",
                            Title = "Chapter 2"
                        });
                });

            modelBuilder.Entity("DinoManga.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("MangaId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MangaId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Comment 1",
                            DateCreated = new DateTime(2023, 11, 12, 16, 39, 41, 825, DateTimeKind.Local).AddTicks(5636),
                            MangaId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("DinoManga.Models.Manga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("ThumbnailImage")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Mangas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            CategoryId = 1,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Description 1",
                            ThumbnailImage = "thumbnail1.jpg",
                            Title = "Manga 1",
                            ViewCount = 0
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            CategoryId = 2,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Description 2",
                            ThumbnailImage = "thumbnail2.jpg",
                            Title = "Manga 2",
                            ViewCount = 0
                        });
                });

            modelBuilder.Entity("DinoManga.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "Password1",
                            Role = "User",
                            Token = "",
                            Username = "User1"
                        });
                });

            modelBuilder.Entity("DinoManga.Models.Chapter", b =>
                {
                    b.HasOne("DinoManga.Models.Manga", "Manga")
                        .WithMany("Chapters")
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manga");
                });

            modelBuilder.Entity("DinoManga.Models.Comment", b =>
                {
                    b.HasOne("DinoManga.Models.Manga", "Manga")
                        .WithMany("Comments")
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DinoManga.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manga");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DinoManga.Models.Manga", b =>
                {
                    b.HasOne("DinoManga.Models.Author", "Author")
                        .WithMany("Mangas")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DinoManga.Models.Category", "Category")
                        .WithMany("Mangas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DinoManga.Models.Author", b =>
                {
                    b.Navigation("Mangas");
                });

            modelBuilder.Entity("DinoManga.Models.Category", b =>
                {
                    b.Navigation("Mangas");
                });

            modelBuilder.Entity("DinoManga.Models.Manga", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DinoManga.Models.User", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
