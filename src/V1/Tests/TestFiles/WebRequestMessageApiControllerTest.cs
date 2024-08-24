using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public abstract class WebRequestMessageApiControllerTest : ApiControllerTest<WebRequestMessageDto>
    {
        [Fact]
        public virtual async Task Update_CreateDateAsync()
        {
            var model = TestManager.GetMinimumDataObject();
            var dto = await CreateBaseAsync(model);

            DateTimeOffset startingCreateDate = dto.CreateDate;

            //Update the CreateDate property
            dto.CreateDate = DateTime.UtcNow;

            //Call Update
            var controller = TestManager.GetController(SystemManager.ServiceProvider);
            var respUpdate = await controller.UpdateAsync(dto);
            if (respUpdate is OkObjectResult okResult)
            {
                Assert.True(okResult.Value != null);
                if (okResult.Value is WebRequestMessageDto obj)
                {
                    Assert.True(obj.CreateDate == startingCreateDate);
                }
                else
                    Assert.Fail("");
            }
            else
                Assert.Fail("");

            // Do cleanup
            await DeleteBaseAsync(dto);
        }
    }
}