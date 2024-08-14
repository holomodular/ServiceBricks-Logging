using AutoMapper;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// This is an automapper profile for the WebRequestMessage domain object.
    /// </summary>
    public partial class WebRequestMessageMappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebRequestMessageMappingProfile()
        {
            // AI: Add mappings for WebRequestMessageDto and WebRequestMessage
            CreateMap<WebRequestMessageDto, WebRequestMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom<KeyResolver>());

            CreateMap<WebRequestMessage, WebRequestMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }

        public class KeyResolver : IValueResolver<DataTransferObject, object, Guid>
        {
            public Guid Resolve(DataTransferObject source, object destination, Guid sourceMember, ResolutionContext context)
            {
                if (string.IsNullOrEmpty(source.StorageKey))
                    return Guid.Empty;

                Guid tempKey;
                if (Guid.TryParse(source.StorageKey, out tempKey))
                    return tempKey;
                return Guid.Empty;
            }
        }
    }
}