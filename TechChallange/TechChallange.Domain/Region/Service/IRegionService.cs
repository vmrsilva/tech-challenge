﻿using System.Linq.Expressions;
using TechChallange.Domain.Region.Entity;

namespace TechChallange.Domain.Region.Service
{
    public interface IRegionService
    {
        Task CreateAsync(RegionEntity regionEntity);
        Task<RegionEntity> GetByIdAsync(Guid id);
        Task<RegionEntity> GetByDdd(string ddd);
        Task DeleteByIdAsync(Guid id);
        Task<IEnumerable<RegionEntity>> GetAllPagedAsync(int pageSize, int page);
        Task UpdateAsync(RegionEntity regionEntity);
        Task<RegionEntity> GetByDddWithContacts(string ddd);
        Task<int> GetCountAsync();
    }
}
