using Moq;
using TechChallange.Domain.Cache;
using TechChallange.Domain.Contact.Entity;
using TechChallange.Domain.Contact.Exception;
using TechChallange.Domain.Contact.Repository;
using TechChallange.Domain.Contact.Service;

namespace TechChallange.Test.Domain.Contact.Service
{
    public class ContactServiceTest
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly Mock<ICacheRepository> _cacheRepositoryMock;
        private readonly ContactService _contactServiceMock;

        public ContactServiceTest()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _cacheRepositoryMock = new Mock<ICacheRepository>();
            _contactServiceMock = new ContactService(_contactRepositoryMock.Object, _cacheRepositoryMock.Object);
        }

        [Fact(DisplayName = "Should Create A New Contact With Success")]
        public async Task ShouldCreateANewContactWithSuccess()
        {
            var contact = new ContactEntity("Test", "12345678", "mail@test.com", Guid.NewGuid());

            await _contactServiceMock.CreateAsync(contact);

            _contactRepositoryMock.Verify(cr => cr.Create(It.IsAny<ContactEntity>()), Times.Once);
        }

        [Fact(DisplayName = "Should Update Throw Exception When Contact Does Not Exist")]
        public async Task ShouldUpdateThrowExceptionWhenContactDoesNotExist()
        {
            _contactRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((ContactEntity)null);

            var contact = new ContactEntity("Test", "12345678", "mail@test.com", Guid.NewGuid());

            await Assert.ThrowsAsync<ContactNotFoundException>(
                () => _contactServiceMock.UpdateAsync(contact));

            _contactRepositoryMock.Verify(cr => cr.UpdateAsync(It.IsAny<ContactEntity>()), Times.Never);
        }

        [Fact(DisplayName = "Should Update When Contact Exist")]
        public async Task ShouldUpdateWhenContactExist()
        {
            var contactMock = new ContactEntity("Test", "12345678", "mail@test.com", Guid.NewGuid());

            _contactRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(contactMock);

            await _contactServiceMock.UpdateAsync(contactMock);

            _contactRepositoryMock.Verify(cr => cr.UpdateAsync(It.IsAny<ContactEntity>()), Times.Once);
        }

        [Fact(DisplayName = "Should Delete Contact Updating Delete Flag")]
        public async Task ShouldDeleteContactUpdatingDeleteFlag()
        {
            var contactMock = new ContactEntity("Test", "12345678", "mail@test.com", Guid.NewGuid());
            var IdMock = Guid.NewGuid();

            _contactRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(contactMock);

            await _contactServiceMock.RemoveByIdAsync(IdMock);

            _contactRepositoryMock.Verify(cr => cr.UpdateAsync(It.IsAny<ContactEntity>()), Times.Once);
        }

        [Fact(DisplayName = "Should Delete Throw Exception When Contact Does Not Exist")]
        public async Task ShouldRemoveThrowExceptionWhenContactDoesNotExist()
        {
            _contactRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((ContactEntity)null);

            var idMock = Guid.NewGuid();

            await Assert.ThrowsAsync<ContactNotFoundException>(
                () => _contactServiceMock.RemoveByIdAsync(idMock));

            _contactRepositoryMock.Verify(cr => cr.UpdateAsync(It.IsAny<ContactEntity>()), Times.Never);
        }

        [Fact(DisplayName = "Should GetById Return Contact")]
        public async Task ShouldGetByIdReturnContact()
        {
            var contactMock = new ContactEntity("Test", "12345678", "mail@test.com", Guid.NewGuid());
            var IdMock = Guid.NewGuid();

            _contactRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(contactMock);

            await _contactServiceMock.GetByIdAsync(IdMock);

            _contactRepositoryMock.Verify(cr => cr.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Should GetById Throw Exception When Contact Does Not Exist")]
        public async Task ShouldGetByIdThrowExceptionWhenContactDoesNotExist()
        {
            var contactMock = new ContactEntity("Test", "12345678", "mail@test.com", Guid.NewGuid());
            var IdMock = Guid.NewGuid();

            _contactRepositoryMock.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((ContactEntity)null);


            await Assert.ThrowsAsync<ContactNotFoundException>(
                        () => _contactServiceMock.GetByIdAsync(IdMock));

            _contactRepositoryMock.Verify(cr => cr.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Should GetAll Return List Of Contacts")]
        public async Task ShouldGetAllReturnListOfContacts()
        {
            var contactMock = new ContactEntity("Test", "12345678", "mail@test.com", Guid.NewGuid());
            var contactsList = new List<ContactEntity> { contactMock };

            _contactRepositoryMock
                .Setup(cr => cr.GetAllAsync())
                .ReturnsAsync(contactsList);

            _cacheRepositoryMock.Setup(c => c.GetAsync<IEnumerable<ContactEntity>>(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<ContactEntity>>>>()))
                                .ReturnsAsync((IEnumerable<ContactEntity>)contactsList);

            var result = await _contactServiceMock.GetAllAsync();

            _cacheRepositoryMock.Verify(c => c.GetAsync<IEnumerable<ContactEntity>>(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<ContactEntity>>>>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(contactMock, result.First());
        }
    }
}
