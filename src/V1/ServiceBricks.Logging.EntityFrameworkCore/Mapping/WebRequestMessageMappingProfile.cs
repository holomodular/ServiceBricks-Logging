using AutoMapper;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// This is an automapper profile for the WebRequestMessage domain object.
    /// </summary>
    public partial class WebRequestMessageMappingProfile : Profile
    {
        /// <summary>
        /// Contructor
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

        /// <summary>
        /// Resolver for the WebRequestMessageDto to WebRequestMessage mapping.
        /// </summary>
        public class KeyResolver : IValueResolver<DataTransferObject, object, long>
        {
            /// <summary>
            /// Resolve the key from the StorageKey property.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="sourceMember"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            public long Resolve(DataTransferObject source, object destination, long sourceMember, ResolutionContext context)
            {
                if (string.IsNullOrEmpty(source.StorageKey))
                    return 0;

                // AI: Parse the value and make sure it is valid
                long tempKey;
                if (long.TryParse(source.StorageKey, out tempKey))
                    return tempKey;
                return 0;
            }
        }
    }
}