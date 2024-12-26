using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallange.Domain.Cache
{
    public interface ICacheWrapper
    {
        Task<string> GetStringAsync(string key, CancellationToken cancellationToken = default);
        Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default);
    }
}
