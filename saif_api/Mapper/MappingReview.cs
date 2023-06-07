using AutoMapper;
using saif_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using saif_api.DTO;

namespace saif_api.Mapper
{
    public class MappingReview : Profile
    {
        public MappingReview()
        {
            CreateMap<CarAdvertisment, Review_DTO>();


            CreateMap<EstateAdvertisment, Review_DTO>();
               //.ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
               //.ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "EstateAdvertisments"));

            CreateMap<ProductAdvertisment, Review_DTO>();
             //.ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
             //.ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "ProductAdvertisments"));

            CreateMap<JobAdvertisment, Review_DTO>();
               //.ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
               //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Sal))
               //.ForMember(dest => dest.price_curr, opt => opt.MapFrom(src => src.sal_curr))
               //.ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "JobAdvertisments"));

            CreateMap<ServiceAdvertisment, Review_DTO>();
               //.ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
               //.ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "ServiceAdvertisments"));

            CreateMap<AnimalAdvertisment, Review_DTO>();
               //.ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
               //.ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "AnimalAdvertisments"));

            CreateMap<AccessoriesAdvertisment, Review_DTO>();
              //.ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
              //.ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "AccessoriesAdvertisments"));

        }
    }
}
