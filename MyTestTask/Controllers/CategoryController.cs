using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.Data;
using MyTestTask.dto.CategoryFolder.Request;
using MyTestTask.dto.CategoryFolder.Response;
using MyTestTask.Models;

namespace MyTestTask.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpPost("PostCategory")]
        public async Task<IActionResult> PostCategory([FromQuery] PostCategoryRequest context)
        {
            var ct = new Categories { Category = context.Category };
            if (ct == null)
            {
                return BadRequest();
            }

            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            if (_db.Categories.Any(x => x.Category == context.Category))
            {
                return BadRequest("Такая категория уже существует");
            }
            await _db.Categories!.AddAsync(ct);
            await _db.SaveChangesAsync();
            return Ok(new PostCategoryResponse { Message = $"Категория {context.Category} успешно создана" });
        }

        [AllowAnonymous]
        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            return Ok(new GetAllCategoryResponse { CategoryList = _db.Categories.ToList() });
        }

        [AllowAnonymous]
        [HttpGet("GetCategory")]
        public Task<IActionResult> GetCity([FromQuery] GetCategoryRequest context)
        {
            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            var result = _db.Categories.Where(x => x.Category == context.Category);
            return result.Any() ? Task.FromResult<IActionResult>(Ok(new GetCategoryResponse { Message = context.Category }))
                : Task.FromResult<IActionResult>(BadRequest("Такой категории нет в бд"));
        }

        [AllowAnonymous]
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromQuery] DeleteCategoryRequest context)
        {
            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            var result = _db.Categories.Where(x => x.Category == context.Category);
            if (!result.Any()) return BadRequest("Такой категории нет в бд");
            if (_db.Subcategories != null && _db.Subcategories.Any(x => x.CategoryId == result.First().Id))
                return BadRequest("Перед удаление категории удалите все подкатегории");
            _db.Remove(result.First());
            await _db.SaveChangesAsync();
            return Ok(new DeleteCategoryResponse { Message = $"Катеория {context.Category} удалена" });
        }

        [AllowAnonymous]
        [HttpPut("UpdateCity")]
        public async Task<IActionResult> UpdateCity([FromQuery] UpdateCategoryRequest context)
        {
            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            var result = _db.Categories.Where(x => x.Category == context.CategoryOld);
            if (!result.Any()) return BadRequest("Такой категории нет в бд");
            result.First().Category = context.CategoryNew;
            _db.Update(result.First());
            await _db.SaveChangesAsync();
            return Ok(new UpdateCategoryResponse() { Message = $"Категория {context.CategoryOld} обновлена и теперь имеет название {context.CategoryNew}" });
        }
    }
}
