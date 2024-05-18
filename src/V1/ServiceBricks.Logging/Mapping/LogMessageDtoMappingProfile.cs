using AutoMapper;

namespace ServiceBricks.Logging
{
    public class NotifyMessageDtoMappingProfile : Profile
    {
        public NotifyMessageDtoMappingProfile() : base()
        {
            CreateMap<ApplicationLogDto, LogMessageDto>()
                .ForMember(x => x.Application, y => y.MapFrom(z => z.Application))
                .ForMember(x => x.Category, y => y.MapFrom(z => z.Category))
                .ForMember(x => x.CreateDate, y => y.MapFrom(z => z.CreateDate))
                .ForMember(x => x.Exception, y => y.MapFrom(z => z.Exception))
                .ForMember(x => x.Level, y => y.MapFrom(z => z.Level))
                .ForMember(x => x.Message, y => y.MapFrom(z => z.Message))
                .ForMember(x => x.Path, y => y.MapFrom(z => z.Path))
                .ForMember(x => x.Properties, y => y.MapFrom(z => z.Properties))
                .ForMember(x => x.Server, y => y.MapFrom(z => z.Server))
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.StorageKey))
                .ForMember(x => x.UserStorageKey, y => y.MapFrom(z => z.UserStorageKey));
        }
    }
}