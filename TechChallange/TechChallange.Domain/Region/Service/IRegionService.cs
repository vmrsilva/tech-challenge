using TechChallange.Domain.Region.Entity;

namespace TechChallange.Domain.Region.Service
{
    public interface IRegionService
    {
        Task Create(RegionEntity regionEntity);

        Task<RegionEntity> GetById(Guid id);

        Task<RegionEntity> GetByDdd(string ddd);

        Task DeleteByDdd(string ddd);
    }
}
