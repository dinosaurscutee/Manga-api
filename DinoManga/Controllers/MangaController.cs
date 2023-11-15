using AutoMapper;
using DinoManga.Data;
using DinoManga.DTO;
using DinoManga.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.OData.Query;

namespace DinoManga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly MangaDbContext _context;
        private readonly IMapper _mapper;
        public MangaController(MangaDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        [Produces("application/json", "text/csv", "application/xml")]
        public async Task<IActionResult> GetAllManga()
        {
            List<Manga> mangas = await _context.Mangas
                .Include(x => x.Author)
                .Include(x => x.Category)
                .ToListAsync();

            List<MangaDTO> mangaDTOs = _mapper.Map<List<MangaDTO>>(mangas);
            return Ok(mangaDTOs.AsQueryable());
        }

        [HttpGet("{id:int}", Name = "GetMangaById")]
        public async Task<IActionResult> GetMangaById(int id)
        {
            Manga manga = await _context.Mangas
                .Include(x => x.Author)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (manga == null)
            {
                return NotFound($"Không tìm thấy manga có ID = {id}");
            }

            MangaDTO mangaDTO = _mapper.Map<MangaDTO>(manga);
            return Ok(mangaDTO);
        }




        [HttpGet("search/{MangaName}")]
        public async Task<IActionResult> GetMangaByName(string MangaName)
        {
            if (string.IsNullOrEmpty(MangaName))
            {
                return BadRequest("Từ khóa tìm kiếm không được để trống");
            }

            List<Manga> mangas = await _context.Mangas
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Where(x => x.Title.Contains(MangaName))
                .ToListAsync();

            List<MangaDTO> mangaDTOs = _mapper.Map<List<MangaDTO>>(mangas);
            return Ok(mangaDTOs);
        }


        [HttpPost]
        public async Task<IActionResult> CreateManga([FromBody] MangaDTO mangaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra và tạo mới Category nếu không tồn tại
            Category category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == mangaDTO.CategoryNames.FirstOrDefault());

            if (category == null)
            {
                category = new Category { Name = mangaDTO.CategoryNames.FirstOrDefault() };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }

            Manga manga = _mapper.Map<Manga>(mangaDTO);
            manga.CategoryId = category.Id;

            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();

            
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true 
            };

            string jsonString = JsonSerializer.Serialize(mangaDTO, jsonSerializerOptions);

            return CreatedAtAction(nameof(GetAllManga), new { id = manga.Id }, jsonString);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManga(int id, [FromBody] MangaDTO mangaDTO)
        {

            Manga mangaToUpdate = await _context.Mangas.FindAsync(id);
            if (!ModelState.IsValid || mangaToUpdate == null)
            {

                return BadRequest(ModelState);
            }


            _mapper.Map(mangaDTO, mangaToUpdate);

            _context.Entry(mangaToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok("update success");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManga(int id)
        {
            Manga mangaToDelete = await _context.Mangas.FindAsync(id);

            if (mangaToDelete == null)
            {
                return NotFound("khong co id");
            }

            _context.Mangas.Remove(mangaToDelete);
            await _context.SaveChangesAsync();

            return Ok("delete success");
        }
    }
}
