using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using MyTestTask.Data;
using MyTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.dto.SubcategoriesFolder.Request;
using MyTestTask.dto.SubcategoriesFolder.Response;

namespace MyTestTask.Controllers
{
    [ApiController]
    [Route("subcategory")]
    public class SubcategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubcategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        [HttpPost("PostSubcategory")]
        public async Task<IActionResult> PostSubcategory([FromQuery] PostSubcategoryRequest context)
        {
            var ctCategory = new Categories { Category = context.Category };
            var ctSubcategory = new Subcategories { Subcategory = context.Subcategory };
            if (ctCategory == null | ctSubcategory == null)
            {
                return BadRequest();
            }

            if (_db.Categories != null &&
                !_db.Categories.Any(x => ctCategory != null && x.Category == ctCategory.Category))
            {
                return BadRequest("Такой категории не сеществует");
            }

            Debug.Assert(_db.Subcategories != null, "_db.Subcategories != null");
            if (_db.Categories != null && _db.Cities != null && _db.Subcategories.Any(x =>
                    ctSubcategory != null && x.Subcategory == ctSubcategory.Subcategory))
            {
                return BadRequest("Такая подкатегория уже сеществует");
            }

            if (_db.Categories != null)
            {
                var category = _db.Categories.First(x => ctCategory != null && x.Category == ctCategory.Category);
                if (_db.Subcategories != null)
                    await _db.Subcategories.AddAsync(new Subcategories
                        { Subcategory = context.Subcategory, CategoryId = category.Id });
            }

            await _db.SaveChangesAsync();
            return Ok(new PostSubcategoryResponse
                { Message = $"В категорию {context.Category} добавлена подкатегория {context.Subcategory}" });
        }
        [AllowAnonymous]
        [HttpGet("GetAllSubcategories")]
        public IActionResult GetAllSubcategories([FromQuery] GetAllSubcategoryRequest context)
        {
            var ctCategory = new Categories { Category = context.Category };
            if (_db.Categories != null &&
                !_db.Categories.Any(x => ctCategory != null && x.Category == ctCategory.Category))
            {
                return BadRequest("Такой категории не сеществует");
            }

            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            var categoryId = _db.Categories.First(x => x.Category == context.Category);
            Debug.Assert(_db.Subcategories != null, "_db.Subcategories != null");
            var result = _db.Subcategories.Where(x => x.CategoryId == categoryId.Id).ToList();
            var responseResult = new GetAllSubcategoryResponse();
            foreach (var zxc in result)
            {
                if (zxc.Subcategory != null) responseResult.Subcategories?.Add(zxc.Subcategory);
            }

            return Ok(responseResult.Subcategories);
        }
        [AllowAnonymous]
        [HttpGet("GetSubcategory")]
        public Task<IActionResult> GetAllSubcategory([FromQuery] GetSubcategoryRequest context)
        {
            var ctCategory = new Categories { Category = context.Category };
            var ctSubcategory = new Subcategories { Subcategory = context.Subcategory };
            if (ctCategory == null | ctSubcategory == null)
            {
                return Task.FromResult<IActionResult>(BadRequest());
            }
            if (_db.Categories != null &&
                !_db.Categories.Any(x => ctCategory != null && x.Category == ctCategory.Category))
            {
                return Task.FromResult<IActionResult>(BadRequest("Такой категории не сеществует"));
            }
            Debug.Assert(_db.Subcategories != null, "_db.Subcategories != null");
            if (_db.Categories != null && _db.Cities != null && !_db.Subcategories.Any(x =>
                    ctSubcategory != null && x.Subcategory == ctSubcategory.Subcategory))
            {
                return Task.FromResult<IActionResult>(BadRequest("Такой подкатегории не сеществует"));
            }
            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            var category = _db.Categories.First(x => ctCategory != null && x.Category == ctCategory.Category);
            return Task.FromResult<IActionResult>(Ok(new GetSubcategoryResponse
                { Message = $"В категории {context.Category} есть подкатегория {context.Subcategory}" }));
        }
        [AllowAnonymous]
        [HttpPut("UpdateSubcategory")]
        public async Task<IActionResult> UpdateSubcategory([FromQuery] UpdateSubcategoryRequest context)
        {
            var ctCategory = new Categories { Category = context.Category };
            var ctSubcategory = new Subcategories { Subcategory = context.SubcategoryNew };
            if (ctCategory == null | ctSubcategory == null)
            {
                return BadRequest();
            }
            if (_db.Categories != null &&
                !_db.Categories.Any(x => ctCategory != null && x.Category == ctCategory.Category))
            {
                return BadRequest("Такой категории не сеществует");
            }
            Debug.Assert(_db.Subcategories != null, "_db.Subcategories != null");
            if (_db.Categories != null && _db.Cities != null && _db.Subcategories.Any(x =>
                    ctSubcategory != null && x.Subcategory == ctSubcategory.Subcategory))
            {
                return BadRequest("Такая подкатегория уже существует");
            }
            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            var category = _db.Categories.First(x => ctCategory != null && x.Category == ctCategory.Category);
            var subcategory = _db.Subcategories.First(x => ctSubcategory != null && x.CategoryId == category.Id);
            subcategory.Subcategory = context.SubcategoryNew;
            await _db.SaveChangesAsync();
            return Ok(new GetSubcategoryResponse
                { Message = $"В категории {context.Category}  подкатегория {context.SubcategoryOld} изменена на {context.SubcategoryNew}"});
        }
        [AllowAnonymous]
        [HttpDelete("DeleteSubcategory")]
        public async Task<IActionResult> DeleteSubcategory([FromQuery] DeleteSubcategoryRequest context)
        {
            var ctCategory = new Categories { Category = context.Category };
            var ctSubcategory = new Subcategories { Subcategory = context.Subcategory };
            if (ctCategory == null | ctSubcategory == null)
            {
                return BadRequest();
            }
            if (_db.Categories != null &&
                !_db.Categories.Any(x => ctCategory != null && x.Category == ctCategory.Category))
            {
                return BadRequest("Такой категории не сеществует");
            }
            Debug.Assert(_db.Subcategories != null, "_db.Subcategories != null");
            if (_db.Categories != null && _db.Cities != null && !_db.Subcategories.Any(x =>
                    ctSubcategory != null && x.Subcategory == ctSubcategory.Subcategory))
            {
                return BadRequest("Такой подкатегории не существует");
            }
            Debug.Assert(_db.Categories != null, "_db.Categories != null");
            var category = _db.Categories.First(x => ctCategory != null && x.Category == ctCategory.Category);
            var subcategory = _db.Subcategories.First(x => ctSubcategory != null && x.CategoryId == category.Id);
            _db.Subcategories.Remove(subcategory);
            await _db.SaveChangesAsync();
            return Ok(new GetSubcategoryResponse
                { Message = $"В категории {context.Category} удалена подкатегория {context.Subcategory}" });
        }
    }
}
