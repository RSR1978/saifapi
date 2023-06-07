using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using saif_api.DTO;
using saif_api.Models;



namespace saif_api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarAdvertisment, SearchResultDTO>()
                .ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
                .ForMember(dest => dest.GoverName,opt => opt.MapFrom(src => GeneratDesc2(src)))
                .ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "وسائط النقل"));

            CreateMap<EstateAdvertisment, SearchResultDTO>()
               .ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
                .ForMember(dest => dest.GoverName, opt => opt.MapFrom(src => GeneratDesc2(src)))
               .ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "عقارات للبيع"));

            CreateMap<ProductAdvertisment, SearchResultDTO>()
             .ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
             .ForMember(dest => dest.GoverName, opt => opt.MapFrom(src => GeneratDesc2(src)))
             .ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "منتجات جديدة و مستعملة"));

            CreateMap<IndustrialAdvertisment, SearchResultDTO>()
            .ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
            .ForMember(dest => dest.GoverName, opt => opt.MapFrom(src => GeneratDesc2(src)))
            .ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "الصناعية و المعدات الثقيلة"));
            
            CreateMap<JobAdvertisment, SearchResultDTO>()
               .ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Sal))
               .ForMember(dest => dest.price_curr, opt => opt.MapFrom(src => src.sal_curr))
               .ForMember(dest => dest.GoverName, opt => opt.MapFrom(src => GeneratDesc2(src)))
               .ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "الوظائف"));

            CreateMap<ServiceAdvertisment, SearchResultDTO>()
               .ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
               .ForMember(dest => dest.GoverName, opt => opt.MapFrom(src => GeneratDesc2(src)))
               .ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "الخدمات"));

            CreateMap<AnimalAdvertisment, SearchResultDTO>()
               .ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
               .ForMember(dest => dest.GoverName, opt => opt.MapFrom(src => GeneratDesc2(src)))
               .ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "الحيوانات"));

            CreateMap<AccessoriesAdvertisment, SearchResultDTO>()
              .ForMember(dest => dest.descTxt, opt => opt.MapFrom(src => GeneratDesc(src)))
              .ForMember(dest => dest.GoverName, opt => opt.MapFrom(src => GeneratDesc2(src)))
              .ForMember(dest => dest.Controller, opt => opt.MapFrom(src => "اكسسوارات"));

        }

        private string GeneratDesc2(CarAdvertisment src)
        {
            return $"{src.Gover.GoverADesc}";
        }

        private string GeneratDesc2(EstateAdvertisment src)
        {
            return $"{src.Gover.GoverADesc}";
        }

        private string GeneratDesc2(ProductAdvertisment src)
        {
            return $"{src.Gover.GoverADesc}";
        }

        private string GeneratDesc2(IndustrialAdvertisment src)
        {
            return $"{src.Gover.GoverADesc}";
        }



        private string GeneratDesc2(JobAdvertisment src)
        {
            return $"{src.Gover.GoverADesc}";
        }

        private string GeneratDesc2(ServiceAdvertisment src)
        {
            return $"{src.Gover.GoverADesc}";
        }

        private string GeneratDesc2(AnimalAdvertisment src)
        {
            return $"{src.Gover.GoverADesc}";
        }

        private string GeneratDesc2(AccessoriesAdvertisment src)
        {
            return $"{src.Gover.GoverADesc}";
        }



        private string GeneratDesc(CarAdvertisment src)
        {
            return $"{src.Color} | {src.Cbody} | {src.Case}";
        }

        private string GeneratDesc(EstateAdvertisment src)
        {
            return $"{src.loc} | {src.Floor} | {src.price}";
        }

        private string GeneratDesc(ProductAdvertisment src)
        {
            return $"{src.loc}  | {src.price}";
        }

        private string GeneratDesc(JobAdvertisment src)
        {
            return $"{src.Subj} | {src.Desc} | {src.JobTyp}";
        }

        private string GeneratDesc(ServiceAdvertisment src)
        {
            return $"{src.Subj} | {src.Desc} | {src.loc}";
        }

        private string GeneratDesc(AnimalAdvertisment src)
        {
            return $"{src.Subj} | {src.Desc} | {src.loc}";
        }

        private string GeneratDesc(AccessoriesAdvertisment src)
        {
            return $"{src.Subj} | {src.Desc} | {src.loc}";
        }
        
        private string GeneratDesc(IndustrialAdvertisment src)
        {
            return $"{src.Subj} | {src.describe} | {src.loc}";
        }
    }
}
