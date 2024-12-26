using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallange.Domain.Region.Entity;
using TechChallange.Domain.Region.Repository;
using TechChallange.Infrastructure.Cache;

namespace TechChallange.Test.Infrastructure.Cache
{
    public class CacheTest
    {
        private readonly Mock<IDistributedCache> _cacheMock;
        private readonly CacheRepository _cacheRepository;

        public CacheTest()
        {
            _cacheMock = new Mock<IDistributedCache>();
            _cacheRepository = new CacheRepository(_cacheMock.Object);
        }

        [Fact]
        public async Task Should()
        {
            var key = "test-key";
            var cachedValue = new RegionEntity("Test", "11");
            var serializedValue = JsonConvert.SerializeObject(cachedValue);

            _cacheMock.Setup(x => x.GetStringAsync(key, default)).ReturnsAsync(serializedValue);

            var result = await _cacheRepository.GetAsync(key, () => Task.FromResult(default(RegionEntity)));

            Assert.NotNull(result);
            Assert.Equal(cachedValue.Name, result.Name);
            Assert.Equal(cachedValue.Ddd, result.Ddd);
            _cacheMock.Verify(x => x.GetStringAsync(key, default), Times.Once);
            _cacheMock.VerifyNoOtherCalls();
        }
    }
}
