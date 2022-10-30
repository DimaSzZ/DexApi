using MyTestTask.Data;
using MyTestTask.dto.Advertisement.Request;
using MyTestTask.Models;

namespace MyTestTask.Services.AdService
{
    public interface IAdController
    {
        void PushAd(ApplicationDbContext _db, PostAdvertisementRequest CreateAdcontext);
        void UpdateAd(ApplicationDbContext _db, UpdateYourAdvertisementRequest UpdateContext);
        void DeleteAd(ApplicationDbContext _db, DeleteYourAdvertisementRequest DeleteContext);
        IQueryable<Ad> GetAllAd(ApplicationDbContext _db);
    }
}
