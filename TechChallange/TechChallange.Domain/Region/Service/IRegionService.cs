using TechChallange.Domain.Region.Entity;

namespace TechChallange.Domain.Region.Service
{
    public interface IRegionService
    {
        Task CreateAsync(RegionEntity regionEntity);
        Task<RegionEntity> GetByIdAsync(Guid id);
        Task<RegionEntity> GetByDdd(string ddd);
        Task DeleteByIdAsync(Guid id);
        Task<IEnumerable<RegionEntity>> GetAllAsync();
        Task UpdateAsync(RegionEntity regionEntity);
    }
}
