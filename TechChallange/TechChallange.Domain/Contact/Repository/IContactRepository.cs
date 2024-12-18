﻿using TechChallange.Domain.Contact.Entity;

namespace TechChallange.Domain.Contact.Repository
{
    public interface IContactRepository
    {
        Task Create(ContactEntity contact);
        Task RemoveByIdAsync(Guid id);

        Task UpdateAsync(ContactEntity contact);

        Task<ContactEntity> GetByIdAsync(Guid id);

        Task<IEnumerable<ContactEntity>> GetByDddAsync(string ddd);
    }
}