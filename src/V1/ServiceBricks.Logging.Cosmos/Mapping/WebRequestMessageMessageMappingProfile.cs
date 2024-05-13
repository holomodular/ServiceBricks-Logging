﻿using AutoMapper;

namespace ServiceBricks.Logging.Cosmos
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
                .ForMember(x => x.Key, y => y.MapFrom<KeyResolver>())
                .ForMember(x => x.RequestUserId, y => y.MapFrom<RequestUserIdResolver>());

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
}