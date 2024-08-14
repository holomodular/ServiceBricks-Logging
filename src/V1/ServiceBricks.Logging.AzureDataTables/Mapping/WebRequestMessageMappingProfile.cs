using AutoMapper;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is a mapping profile for the WebRequestMessage domain object.
    /// </summary>
    public partial class WebRequestMessageMappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebRequestMessageMappingProfile()
        {
            // AI: Create a mapping profile for WebRequestMessageDto and WebRequestMessage.
            CreateMap<WebRequestMessageDto, WebRequestMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.PartitionKey, y => y.MapFrom<PartitionKeyResolver>())
                .ForMember(x => x.RowKey, y => y.MapFrom<RowKeyResolver>())
                .ForMember(x => x.ETag, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore())
                .ForMember(x => x.Key, y => y.Ignore());

            CreateMap<WebRequestMessage, WebRequestMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom<StorageKeyResolver>());
        }
    }
}