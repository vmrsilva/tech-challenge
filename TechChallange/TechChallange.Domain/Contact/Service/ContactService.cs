using TechChallange.Domain.Contact.Entity;
using TechChallange.Domain.Contact.Exception;
using TechChallange.Domain.Contact.Repository;

namespace TechChallange.Domain.Contact.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task Create(ContactEntity contactEntity)
        {
            await _contactRepository.Create(contactEntity).ConfigureAwait(false);
        }

        public Task DeleteByDdd(string ddd)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContactEntity>> GetByDddAsync(string ddd)
        {
            return await _contactRepository.GetByDddAsync(ddd).ConfigureAwait(false);
        }

        public async Task<ContactEntity> GetById(Guid id)
        {
            return await _contactRepository.GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task UpdateAsync(ContactEntity contact)
        {
            var contactDb = await _contactRepository.GetByIdAsync(contact.Id).ConfigureAwait(false);

            if (contactDb == null)
                throw new ContactNotFoundException();

            contactDb.Name = contact.Name;
            contactDb.Phone = contact.Phone;
            contactDb.Email = contact.Email;
            contactDb.RegionId = contact.RegionId;

            await _contactRepository.UpdateAsync(contact).ConfigureAwait(false);
        }
    }
}
