using AutoMapper;

namespace ServiceBricks.Logging.MongoDb
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
            // AI: Map the LogMessageDto to the LogMessage
            CreateMap<LogMessageDto, LogMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom(z => z.StorageKey));

            CreateMap<LogMessage, LogMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }
    }
}