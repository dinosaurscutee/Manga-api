using AutoMapper;
using DInoComic.DTO;
using DInoComic.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MangaDto, Manga>();
        CreateMap<Chapter, ChapterDto>()
            .ForMember(x=>x.MangaName, y =>y.MapFrom(z=> z.Manga.Name))
            .ReverseMap();
    }
}
