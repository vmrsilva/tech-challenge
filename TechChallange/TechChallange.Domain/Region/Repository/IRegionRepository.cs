﻿using TechChallange.Domain.Region.Entity;

namespace TechChallange.Domain.Region.Repository
{
    public interface IRegionRepository
    {
        Task AddAsync(RegionEntity entity);
        Task RemoveByIdAsync(Guid id);
        Task UpdateAsync(RegionEntity entity);
        Task<RegionEntity> GetByIdAsync(Guid id);
        Task<RegionEntity> GetByDddAsync(string ddd);
        Task<IEnumerable<RegionEntity>> GetAllAsync();
        Task<RegionEntity> GetByDddWithContactsAsync(string ddd);

    }
}
