// MapperConfig.cs
using AutoMapper;
using DinoManga.DTO;
using DinoManga.Models;
using System.Collections.Generic;

namespace DinoManga.Map
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Manga, MangaDTO>()
                .ForMember(x => x.AuthorName, y => y.MapFrom(y => y.Author.Name))
                .ForMember(x => x.CategoryNames, y => y.MapFrom(y => new List<string> { y.Category.Name })) // Map Category names
                .ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
