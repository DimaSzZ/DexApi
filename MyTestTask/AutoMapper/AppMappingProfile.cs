using AutoMapper;
using MyTestTask.dto.Advertisement.Response;
using MyTestTask.Models;

namespace MyTestTask.AutoMapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Ad, GetYourAdvertisementResponse>().ReverseMap();
        }
    }
}
