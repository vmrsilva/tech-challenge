﻿using Microsoft.EntityFrameworkCore;
using TechChallange.Domain.Base.Repository;
using TechChallange.Domain.Contact.Entity;
using TechChallange.Domain.Contact.Repository;
using TechChallange.Infrastructure.Context;

namespace TechChallange.Infrastructure.Repository.Contact
{
    public class ContactRepository : IContactRepository
    {
        private readonly IBaseRepository<ContactEntity> _baseRepository;
        private readonly TechChallangeContext _techChallangeContext;

        public ContactRepository(IBaseRepository<ContactEntity> baseRepository, TechChallangeContext techChallangeContext)
        {
            _baseRepository = baseRepository;
            _techChallangeContext = techChallangeContext;
        }

        public async Task Create(ContactEntity contact)
        {
            await _baseRepository.AddAsync(contact).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ContactEntity>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync(c => !c.IsDeleted).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ContactEntity>> GetByDddAsync(string ddd)
        {
            using (_techChallangeContext)
            {
                var contacts = await _techChallangeContext.Contact
                    .Where(c => c.Region.Ddd == ddd && !c.IsDeleted && !c.Region.IsDeleted)
                    .ToListAsync();

                return contacts;
            }
        }

        public async Task<ContactEntity> GetByIdAsync(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task UpdateAsync(ContactEntity contact)
        {
            await _baseRepository.UpdateAsync(contact).ConfigureAwait(false);
        }
    }
}
