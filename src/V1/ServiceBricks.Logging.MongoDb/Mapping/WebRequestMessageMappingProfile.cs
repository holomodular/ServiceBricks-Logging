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
                .ForMember(x => x.Key, y => y.MapFrom(z => z.StorageKey))
                .ForMember(x => x.RequestUserId, y => y.MapFrom<RequestUserIdResolver>());

            CreateMap<WebRequestMessage, WebRequestMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }
    }

    public class RequestUserIdResolver : IValueResolver<WebRequestMessageDto, object, Guid?>
    {
        public Guid? Resolve(WebRequestMessageDto source, object destination, Guid? sourceMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.RequestUserId))
                return new Nullable<Guid>();

            Guid tempKey;
            if (Guid.TryParse(source.RequestUserId, out tempKey))
                return tempKey;
            return new Nullable<Guid>();
        }
    }
}