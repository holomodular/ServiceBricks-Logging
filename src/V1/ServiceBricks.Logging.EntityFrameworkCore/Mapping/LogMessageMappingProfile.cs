using AutoMapper;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// This is an automapper profile for the LogMessage domain object.
    /// </summary>
    public class LogMessageMappingProfile : Profile
    {
        public LogMessageMappingProfile()
        {
            CreateMap<LogMessageDto, LogMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom<KeyResolver>());

            CreateMap<LogMessage, LogMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }

        public class KeyResolver : IValueResolver<DataTransferObject, object, long>
        {
            public long Resolve(DataTransferObject source, object destination, long sourceMember, ResolutionContext context)
            {
                if (string.IsNullOrEmpty(source.StorageKey))
                    return 0;

                long tempKey;
                if (long.TryParse(source.StorageKey, out tempKey))
                    return tempKey;
                return 0;
            }
        }
    }
}