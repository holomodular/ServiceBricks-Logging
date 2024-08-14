using AutoMapper;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// This is an automapper profile for the LogMessage domain object.
    /// </summary>
    public partial class LogMessageMappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LogMessageMappingProfile()
        {
            // AI: Add mappings for LogMessageDto and LogMessage
            CreateMap<LogMessageDto, LogMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom<KeyResolver>());

            CreateMap<LogMessage, LogMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }

        /// <summary>
        /// Key resolver for the LogMessageDto to LogMessage mapping.
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