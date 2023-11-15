// CategoryController.cs
using AutoMapper;
using DinoManga.Data;
using DinoManga.DTO;
using DinoManga.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DinoManga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MangaDbContext _context;
        private readonly IMapper _mapper;

        public CategoryController(MangaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        [Produces("application/json", "application/xml", "text/csv")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);

            if (Request.Headers.ContainsKey("Accept") && Request.Headers["Accept"].ToString().Contains("text/csv"))
            {
                var csvdata = new StringBuilder();
                foreach (var item in categoriesDTO)
                {
                    csvdata.AppendLine($"{item.Id};{item.Name}");
                }
                var csv = new ContentResult
                {
                    Content = csvdata.ToString(),
                    ContentType = "text/csv",
                    StatusCode = 200
                };
                return csv;
            }
            else
            {
                if (Request.Headers.ContainsKey("Accept") && Request.Headers["Accept"].ToString().Contains("application/xml"))
                {
                    var xmlData = new ObjectResult(categoriesDTO)
                    {
                        StatusCode = 200,
                        ContentTypes = new MediaTypeCollection { "application/xml" }
                    };
                    return xmlData;
                }
                // Mặc định trả về JSON
                else
                {
                    return Ok(categoriesDTO);
                }

            }
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid category data");
            }

            var category = _mapper.Map<Category>(categoryDTO);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, categoryDTO);
        }
    }
}
