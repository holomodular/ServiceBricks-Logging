using AutoMapper;

namespace ServiceBricks.Logging.Cosmos
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
        /// Resolve the key from the storage key.
        /// </summary>
        public class KeyResolver : IValueResolver<DataTransferObject, object, Guid>
        {
            /// <summary>
            /// Resolve the key from the storage key.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="sourceMember"></param>
            /// <param name="context"></param>
            /// <returns></returns>
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