using AutoMapper;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is a mapping profile for the LogMessage domain object.
    /// </summary>
    public partial class LogMessageMappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LogMessageMappingProfile()
        {
            // AI: Create a mapping profile for LogMessageDto and LogMessage.
            CreateMap<LogMessageDto, LogMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.PartitionKey, y => y.MapFrom<PartitionKeyResolver>())
                .ForMember(x => x.RowKey, y => y.MapFrom<RowKeyResolver>())
                .ForMember(x => x.ETag, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore())
                .ForMember(x => x.Key, y => y.Ignore());

            CreateMap<LogMessage, LogMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom<StorageKeyResolver>());
        }
    }
}