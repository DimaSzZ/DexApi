using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.Data;
using MyTestTask.dto.Advertisement.Request;
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
        [HttpPost("CreateAd")]
        public async Task<IActionResult> AddAd([FromBody] PostAdvertisementRequest CreateAdcontext)
        {
            var ct = new Ad
            {
                Number = CreateAdcontext.Number,
                Description = CreateAdcontext.Description,
                Page = CreateAdcontext.Page,
                Rating = CreateAdcontext.Rating,
                PublicationDate = DateTimeOffset.Now.UtcDateTime,
                ExpirationDate = DateTimeOffset.Now.UtcDateTime.AddDays(5)
            };

            var personId = Request.Cookies["Id"];
            if (!string.IsNullOrEmpty(personId))
                ct.PersonId = Guid.Parse(personId);
            else
                return BadRequest("Что то не так с Id пользователя  ");

            await _db.Ad.AddAsync(ct);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpPut("UpdateAd")]
        public async Task<IActionResult> UpdateAd([FromBody] UpdateYourAdvertisementRequest UpdateContext)
        {
            var advert = _db.Ad.First(x => x.Id == UpdateContext.Id && x.PersonId == Guid.Parse(Request.Cookies["Id"] ?? string.Empty));
            advert.Number = UpdateContext.Number;
            advert.Description = UpdateContext.Description;
            advert.Page = UpdateContext.Page;
            advert.Rating = UpdateContext.Rating;
            await _db.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpDelete("DeleteAd")]
        public async Task<IActionResult> DeleteAd([FromBody] DeleteYourAdvertisementRequest DeleteContext)
        {
            var advert = _db.Ad.First(x => x.Id == DeleteContext.Id && x.PersonId == Guid.Parse(Request.Cookies["Id"] ?? string.Empty));
            _db.Ad.Remove(advert);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpGet("GetYourAllAd")]
        public async Task<IActionResult> GetYourAdvertisements()
        {
            var advert = _db.Ad.Where(x => x.PersonId == Guid.Parse(Request.Cookies["Id"] ?? string.Empty));

            return Ok(advert.ToList());
        }
    }
}

