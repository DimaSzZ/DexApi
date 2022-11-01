using MyTestTask.Data;
using MyTestTask.dto.Advertisement.Request;
using MyTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace MyTestTask.Services.AdService
{
    public class AdControllerLogic : IAdController
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AdControllerLogic(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public async Task<string> DeleteAd(ApplicationDbContext _db, DeleteYourAdvertisementRequest DeleteContext)
        {
            var advert = _db.Ad.First(x => x.Id == DeleteContext.Id && x.PersonId == Guid.Parse(_contextAccessor.HttpContext.Request.Cookies["Id"] ?? string.Empty));
            _db.Ad.Remove(advert);
            await _db.SaveChangesAsync();
            return "Объявление удалено";
        }
        public async Task<List<Ad>> GetAllAd(ApplicationDbContext _db)
        {
            var advert = await _db.Ad.Where(x => x.PersonId == Guid.Parse(_contextAccessor.HttpContext.Request.Cookies["Id"] ?? string.Empty)).ToListAsync();
            return advert;
        }

        public async Task<string> PushAd(ApplicationDbContext _db, PostAdvertisementRequest CreateAdcontext)
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

            var personId = _contextAccessor.HttpContext.Request.Cookies["Id"];
            if (!string.IsNullOrEmpty(personId))
                ct.PersonId = Guid.Parse(personId);
            else
                return null; ;

            await _db.Ad.AddAsync(ct);
            await _db.SaveChangesAsync();
            return "Запрос прошел удачно";
        }

        public async Task<string> UpdateAd(ApplicationDbContext _db, UpdateYourAdvertisementRequest UpdateContext)
        {
            var advert = _db.Ad.First(x => x.Id == UpdateContext.Id && x.PersonId == Guid.Parse(_contextAccessor.HttpContext.Request.Cookies["Id"] ?? string.Empty));
            advert.Number = UpdateContext.Number;
            advert.Description = UpdateContext.Description;
            advert.Page = UpdateContext.Page;
            advert.Rating = UpdateContext.Rating;
            await _db.SaveChangesAsync();
            return "Обновили объявление";
        }
    }
}
