using TechChallange.Domain.Base.Repository;
using TechChallange.Domain.Cache;
using TechChallange.Domain.Contact.Repository;
using TechChallange.Domain.Region.Entity;
using TechChallange.Domain.Region.Exception;
using TechChallange.Domain.Region.Repository;

namespace TechChallange.Domain.Region.Service
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICacheRepository _cacheRepository;

        public RegionService(IRegionRepository regionRepository, ICacheRepository cacheRepository)
        {
            _regionRepository = regionRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task CreateAsync(RegionEntity regionEntity)
        {
            await _regionRepository.AddAsync(regionEntity).ConfigureAwait(false);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var regionDb = await _regionRepository.GetByIdAsync(id).ConfigureAwait(false);

            if (regionDb == null)
                throw new RegionNotFoundException();

            regionDb.MarkAsDeleted();

            await _regionRepository.UpdateAsync(regionDb).ConfigureAwait(false);

        }

        public async Task<IEnumerable<RegionEntity>> GetAllAsync()
        {
            return await _cacheRepository.GetValueAsync("allRegions", async () => await _regionRepository.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<RegionEntity> GetByDdd(string ddd)
        {
            return await _regionRepository.GetByDddAsync(ddd).ConfigureAwait(false);
        }

        public async Task<RegionEntity> GetByIdAsync(Guid id)
        {
            return await _regionRepository.GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task UpdateAsync(RegionEntity regionEntity)
        {
            var regionDb = await GetByIdAsync(regionEntity.Id).ConfigureAwait(false);

            if (regionDb == null)
                throw new RegionNotFoundException();

            regionDb.Name = regionEntity.Name;
            regionDb.Ddd = regionEntity.Ddd;

            await _regionRepository.UpdateAsync(regionDb).ConfigureAwait(false);
        }
    }
}
