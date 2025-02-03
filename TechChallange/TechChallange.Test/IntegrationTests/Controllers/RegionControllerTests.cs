using System.Net;
using System.Text.Json;
using TechChallange.Api.Controllers.Region.Dto;
using TechChallange.Api.Response;

namespace TechChallange.Test.IntegrationTests.Controllers
{
    public class RegionControllerTests : IClassFixture<TechChallangeApplicationFactory>
    {
        private readonly TechChallangeApplicationFactory _techChallangeApplicationFactory;

        public RegionControllerTests(TechChallangeApplicationFactory techChallangeApplicationFactory)
        {
            _techChallangeApplicationFactory = techChallangeApplicationFactory;
        }

        [Fact]
        public async Task Test()
        {
            var client = _techChallangeApplicationFactory.CreateClient();

            var response = await client.GetAsync("region/get-all?pageSize=10&page=1");

            var resp = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponsePagedDto<IEnumerable<RegionResponseDto>>>(resp,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result?.Data);
        }
    }
}
