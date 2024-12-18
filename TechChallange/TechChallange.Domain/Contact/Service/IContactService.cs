using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallange.Domain.Contact.Entity;
using TechChallange.Domain.Region.Entity;

namespace TechChallange.Domain.Contact.Service
{
    public interface IContactService
    {
        Task Create(ContactEntity contactEntity);

        Task<ContactEntity> GetById(Guid id);

        Task<IEnumerable<ContactEntity>> GetByDddAsync(string ddd);

        Task DeleteByDdd(string ddd);
        Task UpdateAsync(ContactEntity contact);

    }
}
