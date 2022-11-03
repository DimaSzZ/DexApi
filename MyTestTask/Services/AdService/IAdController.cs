using MyTestTask.Data;
using MyTestTask.dto.Advertisement.Request;
using MyTestTask.Models;

namespace MyTestTask.Services.AdService
{
    ///<summary>
    ///Интерфейс, созданный для AdControllerLogic
    ///</summary>
    public interface IAdController
    {
        Task<string> PushAd(ApplicationDbContext _db, PostAdvertisementRequest CreateAdcontext);
        Task<string> UpdateAd(ApplicationDbContext _db, UpdateYourAdvertisementRequest UpdateContext);
        Task<string> DeleteAd(ApplicationDbContext _db, DeleteYourAdvertisementRequest DeleteContext);
        Task<List<Ad>> GetAllAd(ApplicationDbContext _db);
    }
}
