using AutoMapper;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is an automapper profile for the WebRequestMessage domain object.
    /// </summary>
    public class WebRequestMessageMappingProfile : Profile
    {
        public WebRequestMessageMappingProfile()
        {
            CreateMap<WebRequestMessageDto, WebRequestMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom(z => z.StorageKey));

            CreateMap<WebRequestMessage, WebRequestMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }
    }
}