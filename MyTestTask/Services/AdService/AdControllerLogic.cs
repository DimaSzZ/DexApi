using MyTestTask.Data;
using MyTestTask.dto.Advertisement.Request;
using MyTestTask.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MyTestTask.Services.AdService
{
    ///<summary>
    ///Это сервис для реализации логики всех апи в AdvertisementController
    ///</summary>
    public class AdControllerLogic : IAdController
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        ///<summary>
        ///Конструктор, прнимаюший contextAccessor для дальнейшего использование куки файлов
        ///</summary>
        public AdControllerLogic(IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
        ///<summary>
        ///Метод, реализующий логику для удаления объявления и возвращения результата в метод контроллера
        ///</summary>
        public async Task<string> DeleteAd(ApplicationDbContext _db, DeleteYourAdvertisementRequest DeleteContext)
        {
            var advert = _db.Ad.First(x => x.Id == DeleteContext.Id && x.PersonId == Guid.Parse(_contextAccessor.HttpContext.Request.Cookies["Id"] ?? string.Empty));
            if (advert == null) return null;
            _db.Ad.Remove(advert);
            await _db.SaveChangesAsync();
            return "Объявление удалено";
        }
        ///<summary>
        ///Метод, реализующий логику для получения все объявлений и возвращения результата в метод контроллера
        ///</summary>
        public async Task<List<Ad>> GetAllAd(ApplicationDbContext _db)
        {
            var advert = await _db.Ad.Where(x => x.PersonId == Guid.Parse(_contextAccessor.HttpContext.Request.Cookies["Id"] ?? string.Empty)).ToListAsync();
            if (advert == null) return null;
            return advert;
        }
        ///<summary>
        ///Метод, реализующий логику для добавления объявления и возвращения результата в метод контроллера
        ///</summary>
        public async Task<string> PushAd(ApplicationDbContext _db, PostAdvertisementRequest CreateAdcontext)
        {
            var ct = _mapper.Map<Ad>(CreateAdcontext);

            var personId = _contextAccessor.HttpContext.Request.Cookies["Id"];
            if (!string.IsNullOrEmpty(personId))
                ct.PersonId = Guid.Parse(personId);
            else
                return null;
            var MaxAdConfig = int.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["MaxAdConfig"]);
            if (_db.Ad.Where(_ => _.PersonId == Guid.Parse(personId)).ToList().Count == MaxAdConfig) 
                return null;

            await _db.Ad.AddAsync(ct);
            await _db.SaveChangesAsync();
            return "Запрос прошел удачно";
        }
        ///<summary>
        ///Метод, реализующий логику для обновления объявления и возвращения результата в метод контроллера
        ///</summary>
        public async Task<string> UpdateAd(ApplicationDbContext _db, UpdateYourAdvertisementRequest UpdateContext)
        {
            var advert = _db.Ad.First(x => x.Id == UpdateContext.Id && x.PersonId == Guid.Parse(_contextAccessor.HttpContext.Request.Cookies["Id"] ?? string.Empty));
            if (advert == null) return null;
            advert.Number = UpdateContext.Number;
            advert.Description = UpdateContext.Description;
            advert.Page = UpdateContext.Page;
            advert.Rating = UpdateContext.Rating;
            await _db.SaveChangesAsync();
            return "Обновили объявление";
        }
    }
}
