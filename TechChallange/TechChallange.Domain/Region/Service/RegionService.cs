using TechChallange.Domain.Base.Repository;
using TechChallange.Domain.Region.Entity;
using TechChallange.Domain.Region.Repository;

namespace TechChallange.Domain.Region.Service
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task Create(RegionEntity regionEntity)
        {
            await _regionRepository.AddAsync(regionEntity).ConfigureAwait(false);
        }

        public async Task DeleteByDdd(string ddd)
        {
            var regionDb = await _regionRepository.GetByDddAsync(ddd).ConfigureAwait(false);

            if (regionDb != null)
            {
                regionDb.MarkAsDeleted();

                await _regionRepository.UpdateAsync(regionDb).ConfigureAwait(false);    
            }
        }

        public async Task<RegionEntity> GetByDdd(string ddd)
        {
            return await _regionRepository.GetByDddAsync(ddd).ConfigureAwait(false);
        }

        public async Task<RegionEntity> GetById(Guid id)
        {
            return await _regionRepository.GetByIdAsync(id).ConfigureAwait(false);
        }
    }
}
