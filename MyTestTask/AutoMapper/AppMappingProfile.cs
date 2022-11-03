using AutoMapper;
using MyTestTask.dto.Advertisement.Request;
using MyTestTask.dto.Advertisement.Response;
using MyTestTask.Models;

namespace MyTestTask.AutoMapper
{
    ///<summary>
    ///Автомапер
    ///</summary>
    public class AppMappingProfile : Profile
    {
        ///<summary>
        ///Конструктор,добавляющий мапинг в проект
        ///</summary>
        public AppMappingProfile()
        {
            CreateMap<Ad, GetYourAdvertisementResponse>().ReverseMap();
            CreateMap<Ad, PostAdvertisementRequest>().ReverseMap();
        }
    }
}
