using AutoMapper;
using DTOS;
using Models;

namespace Map;

public static class Mapping
{
    public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {             
                cfg.CreateMap<Logger, LoggerDto>()
                .ForMember(dest => dest.loggerID, opt => opt.MapFrom(src => src.loggerID))
                .ForMember(dest => dest.Inicio, opt => opt.MapFrom(src => src.Inicio))
                .ForMember(dest => dest.Fim, opt => opt.MapFrom(src => src.Fim))
                .ForMember(dest => dest.Inicio, opt => opt.MapFrom(src => src.Inicio))
                .ReverseMap()
                ;
                cfg.CreateMap<Logger, LoggerInsertDto>()
                .ReverseMap()
                ;

            });
            var mapper = new Mapper(config);
            return mapper;
        }
    
}
