using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallange.Domain.Cache
{
    public interface ICacheRepository
    {
        Task<T> GetValueAsync<T>(string key);
        Task<T> GetValueAsync<T>(string key, Func<Task<T>> producer);

        Task SetValueAsync<T>(string key, T t);
    }
}
