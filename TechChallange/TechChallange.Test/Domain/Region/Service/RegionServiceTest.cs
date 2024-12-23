﻿using Moq;
using TechChallange.Domain.Cache;
using TechChallange.Domain.Contact.Exception;
using TechChallange.Domain.Region.Entity;
using TechChallange.Domain.Region.Exception;
using TechChallange.Domain.Region.Repository;
using TechChallange.Domain.Region.Service;

namespace TechChallange.Test.Domain.Region.Service
{
    public class RegionServiceTest
    {
        private readonly Mock<IRegionRepository> _regionRepositoryMock;
        private readonly Mock<ICacheRepository> _cacheRepositoryMock;
        private readonly RegionService _regionServiceMock;

        public RegionServiceTest()
        {
            _regionRepositoryMock = new Mock<IRegionRepository>();
            _cacheRepositoryMock = new Mock<ICacheRepository>();
            _regionServiceMock = new RegionService(_regionRepositoryMock.Object, _cacheRepositoryMock.Object);
        }

        [Fact(DisplayName = "Should Create A Region")]
        public async Task ShouldCreateARegion()
        {
            var regionMock = new RegionEntity("Test", "11");

            await _regionServiceMock.CreateAsync(regionMock);

            _regionRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<RegionEntity>()), Times.Once);
        }

        [Fact(DisplayName = "Should Delete A Region")]
        public async Task ShouldDeleteARegion()
        {
            var idMock = Guid.NewGuid();
            var regionMock = new RegionEntity("Test", "11");

            _regionRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((regionMock));

            await _regionServiceMock.DeleteByIdAsync(idMock);

            _regionRepositoryMock.Verify(rr => rr.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _regionRepositoryMock.Verify(rr => rr.UpdateAsync(It.IsAny<RegionEntity>()), Times.Once);
        }

        [Fact(DisplayName = "Should Delete Throw Exception When Region Does Not Exist")]
        public async Task ShouldDeleteThrowExceptionWhenRegionDoesNotExist()
        {
            var idMock = Guid.NewGuid();
            var regionMock = new RegionEntity("Test", "11");

            _regionRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((RegionEntity)null);


            await Assert.ThrowsAsync<RegionNotFoundException>(
                () => _regionServiceMock.DeleteByIdAsync(idMock));

            _regionRepositoryMock.Verify(rr => rr.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _regionRepositoryMock.Verify(rr => rr.UpdateAsync(It.IsAny<RegionEntity>()), Times.Never);
        }

        [Fact(DisplayName = "Should Update A Region")]
        public async Task ShouldUpdateARegion()
        {
            var regionMock = new RegionEntity("Test", "11");

            _regionRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(regionMock);

            await _regionServiceMock.UpdateAsync(regionMock);

            _regionRepositoryMock.Verify(rr => rr.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _regionRepositoryMock.Verify(rr => rr.UpdateAsync(It.IsAny<RegionEntity>()), Times.Once);
        }


        [Fact(DisplayName = "Should Update Throw Exception When Region Does Not Exist")]
        public async Task ShouldUpdateThrowExceptionWhenRegionDoesNotExist()
        {
            var regionMock = new RegionEntity("Test", "11");

            _regionRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((RegionEntity)null);

            await Assert.ThrowsAsync<RegionNotFoundException>(
                () => _regionServiceMock.UpdateAsync(regionMock));

            _regionRepositoryMock.Verify(rr => rr.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _regionRepositoryMock.Verify(rr => rr.UpdateAsync(It.IsAny<RegionEntity>()), Times.Never);
        }
    }
}
