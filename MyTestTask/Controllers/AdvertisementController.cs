using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.Data;
using MyTestTask.dto.AdvertisementFolder.Request;
using MyTestTask.dto.AdvertisementFolder.Response;
using MyTestTask.Models;

namespace MyTestTask.Controllers
{
    [ApiController]
    [Route("Advertisement")]
    public class AdvertisementController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdvertisementController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpPost("PostAdvertisement")]
        public async Task<IActionResult> Post([FromBody] PostAdvertisementRequest context)
        {
            var ct = new Advertising
            {
                Name = context.Name,
                Price = context.Price,
                Number = context.Number,
                City = context.City,
                Category = context.Category,
                Subcategory = context.Subcategory,
                Description = context.Description,
                PublicationDate = DateTimeOffset.Now.UtcDateTime
            };

            if (ct == null)
                return BadRequest();

            if (ct.Subcategory != null && ct.Category == null)
                return BadRequest("Подкатегории без категории быть не может");
            if (_db.Persons != null && !_db.Persons.Any(x => x.Number == ct.Number))
                return BadRequest("Такого подтвержденного номера не существует");

            if (_db.Cities != null && !_db.Cities.Any(x => x.City == ct.City))
                return BadRequest("Такого города нет в бд");

            if (_db.Categories != null && !_db.Categories.Any(x => x.Category == ct.Category))
                return BadRequest("Такой категории не существует");

            if (_db.Subcategories != null && !_db.Subcategories.Any(x => x.Subcategory == ct.Subcategory))
                return BadRequest("Такого подкатегории  не существует");

            var personId = Request.Cookies["Id"];
            if (!string.IsNullOrEmpty(personId))
                ct.PersonId = int.Parse(personId);
            else
                return BadRequest("Что то не так с Id пользователя  ");

            Debug.Assert(_db.Advertisings != null, "_db.Advertisings != null");
            await _db.Advertisings.AddAsync(ct);
            await _db.SaveChangesAsync();
            return Ok(new PostAdvertisementResponse { Message = $"Объялвение добавлено" });
        }
        [Authorize]
        [HttpPut("UpdateAdvertisement")]
        public async Task<IActionResult> Put([FromBody] UpdateYourAdvertisementRequest ct)
        {
            
            if (ct == null)
                return BadRequest();

            if (ct.NewSubcategory != null && ct.NewCategory == null)
                return BadRequest("Подкатегории без категории быть не может");
            if (_db.Persons != null && !_db.Persons.Any(x => x.Number == ct.NewNumber))
                return BadRequest("Такого подтвержденного номера не существует");

            if (_db.Cities != null && !_db.Cities.Any(x => x.City == ct.NewCity))
                return BadRequest("Такого города нет в бд");

            if (_db.Categories != null && !_db.Categories.Any(x => x.Category == ct.NewCategory))
                return BadRequest("Такой категории не существует");

            if (_db.Subcategories != null && !_db.Subcategories.Any(x => x.Subcategory == ct.NewSubcategory))
                return BadRequest("Такого подкатегории  не существует");
            Debug.Assert(_db.Advertisings != null, "_db.Advertisings != null");
            var advert = _db.Advertisings.First(x => x.Id == ct.Id && x.PersonId == int.Parse(Request.Cookies["Id"] ?? string.Empty));
            Debug.Assert(_db.Advertisings != null, "_db.Advertisings != null");
            advert.Name = ct.NewName;
            advert.Price = ct.NewPrice;
            advert.Number = ct.NewNumber;
            advert.City = ct.NewCity;
            advert.Category = ct.NewCategory;
            advert.Subcategory = ct.NewSubcategory;
            advert.Description = ct.NewDescription;
            await _db.SaveChangesAsync();
            return Ok(new PostAdvertisementResponse { Message = $"Ваше объялвение обновлено" });
        }
        [Authorize]
        [HttpDelete("DeleteAdvertisement")]
        public async Task<IActionResult> Delete([FromBody] DeleteYourAdvertisementRequest ct)
        {
            Debug.Assert(_db.Advertisings != null, "_db.Advertisings != null");
            var advert = _db.Advertisings.First(x => x.Id == ct.Id && x.PersonId == int.Parse(Request.Cookies["Id"] ?? string.Empty));
            _db.Advertisings.Remove(advert);
            await _db.SaveChangesAsync();
            return Ok(new PostAdvertisementResponse { Message = $"Ваше объялвение удалено" });
        }
        [Authorize]
        [HttpGet("GetYourAdvertisements")]
        public Task<IActionResult> GetYourAdvertisements()
        {
            Debug.Assert(_db.Advertisings != null, "_db.Advertisings != null");
            var advert = _db.Advertisings.Where(x => x.PersonId == int.Parse(Request.Cookies["Id"] ?? string.Empty));
            var AllAdvert = new List<GetAdvertisementResponse>();
            foreach (var ct in advert)
            {
                AllAdvert.Add(new GetAdvertisementResponse
                {
                    Id = ct.Id,
                    Name = ct.Name,
                    Price = ct.Price,
                    Number = ct.Number,
                    City = ct.City,
                    Category = ct.Category,
                    Subcategory = ct.Subcategory,
                    Description = ct.Description,
                    PublicationDate = ct.PublicationDate
                });
            }
            return Task.FromResult<IActionResult>(Ok(AllAdvert));
        }
    }
}

