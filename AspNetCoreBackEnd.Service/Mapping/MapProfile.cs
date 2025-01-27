using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.viewModels;
using AutoMapper;


namespace AspNetCoreBackEnd.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {



            CreateMap<HaberVM, Haberler>().ReverseMap();
            CreateMap<HaberUpdateVM, Haberler>().ReverseMap();


            CreateMap<TextUpdateVM, Texts>().ReverseMap();


            CreateMap<FormVM, Forms>().ReverseMap();



            CreateMap<FirmaVM, Firmalar>().ReverseMap();
            CreateMap<FirmaUpdateVM, Firmalar>().ReverseMap();


            CreateMap<FirmaResimVM, FirmaResimleri>().ReverseMap();
            CreateMap<FirmaResimUpdateVM, FirmaResimleri>().ReverseMap();

            CreateMap<FirmaUrunVM, FirmaUrunler>().ReverseMap();
            CreateMap<FirmaUrunUpdateVM, FirmaUrunler>().ReverseMap();


        }
    }
}
