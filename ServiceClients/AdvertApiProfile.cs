using AdvertAPI.Models;
using AutoMapper;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Models.Home;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertApiProfile : Profile
    {
        public AdvertApiProfile()
        {
            CreateMap<AdvertModel, CreateAdvertModel>().ReverseMap();
            CreateMap<CreateAdvertResponse, AdvertResponse>().ReverseMap();
            CreateMap<ConfirmAdvertRequest, ConfirmAdvertModel>().ReverseMap();
            CreateMap<CreateAdvertModel, CreateAdvertViewModel>().ReverseMap();
            CreateMap<AdvertModel, Advertisement>().ReverseMap();
            CreateMap<AdvertType, SearchViewModel>().ReverseMap();
            CreateMap<IndexViewModel, Advertisement>().ReverseMap(); 
        }
    }
}
