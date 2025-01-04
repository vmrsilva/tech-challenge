using TechChallange.Domain.Base.Repository;
using TechChallange.Domain.Region.Entity;
using TechChallange.Domain.Region.Repository;

namespace TechChallange.Infrastructure.Repository.Region
{
    public class RegionRepository : IRegionRepository
    {
        private readonly IBaseRepository<RegionEntity> _baseRepository;

        public RegionRepository(IBaseRepository<RegionEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddAsync(RegionEntity entity)
        {
            await _baseRepository.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task<IEnumerable<RegionEntity>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync(r => !r.IsDeleted);
        }

        public async Task<RegionEntity> GetByDddAsync(string ddd)
        {
            return await _baseRepository.GetAsync(r => r.Ddd == ddd).ConfigureAwait(false);
        }

        public async Task<RegionEntity> GetByDddWithContactsAsync(string ddd)
        {
            return await _baseRepository.GetOneWithIncludeAsync(r => r.Ddd == ddd, r => r.Contacts).ConfigureAwait(false);
        }

        public async Task<RegionEntity> GetByIdAsync(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id).ConfigureAwait(false);    
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            await _baseRepository.RemoveByIdAsync(id).ConfigureAwait(false);
        }

        public async Task UpdateAsync(RegionEntity entity)
        {
            await _baseRepository.UpdateAsync(entity).ConfigureAwait(false);
        }
    }
}
