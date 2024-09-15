using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceQuery;

namespace ServiceBricks.Xunit
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseApiClientTests : ApiClientTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseApiControllerTests : ApiControllerTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseApiServiceTests : ApiServiceTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseBusinessRuleTests : BusinessRuleTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseDomainQueueProcessTests : DomainQueueProcessTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseDomainRepositoryTests : DomainRepositoryTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseExtensionTests : ExtensionTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseIpAddressServiceTests : IpAddressServiceTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseMappingTests : MappingTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseMiddlewareTests : MiddlewareTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseObjectTests : ObjectTests
    {
    }

    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public partial class BaseTimezoneServiceTests : TimezoneServiceTests
    {
    }
}