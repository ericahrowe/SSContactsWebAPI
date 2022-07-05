using LiteDB;
using NUnit.Framework;
using SSContacts.Core.Entities;
using SSContacts.Infrastructure.Data;
using SSContacts.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSContacts.Infrastructure.Tests
{
    public class ContactRepositoryTests
    {
        private ContactRepository _repository;

        [SetUp]
        public void Setup()
        {
            string connectionString = ":memory:";
            BsonMapper.Global.EnumAsInteger = true;
            _repository = new ContactRepository(connectionString);
            _repository.DeleteMany(c => c.Id > 0);
            DatabasePopulator.PopulateDatabase(_repository);
        }

        [Test]
        public async Task GetById_ShouldReturnContact_GivenValidId()
        {
            var contactId = 1;
            var result = await _repository.GetByIdAsync(contactId);

            Assert.That(contactId == result.Id);
        }

        [Test]
        public async Task ListAsync_ShouldReturnContactList_GivenDataInDatabase()
        {
            var testContacts = 3;
            var result = await _repository.ListAsync();

            Assert.That(result.Count == testContacts);
        }

        [Test]
        public async Task AddAsync_ShouldAddContact_GivenValidNewContact()
        {
            var newContact = new Contact
            {
                Name = new Name("Joe", "Isa", "Tester"),
                Address = new Address("123 Main St", "Sometown", "New Jersey", "22222"),
                Phone = new List<Phone>()
                {
                    new Phone("234-567-1234",PhoneType.Home ),
                },
                Email = "joe.isa.tester@gmail.com"
            };

            var result = await _repository.AddAsync(newContact);

            Assert.That(result.Name.First == newContact.Name.First);
            Assert.That(result.Id > 0);
            Assert.IsTrue(_repository.ListAsync().Result.Any(c => c.Email == "joe.isa.tester@gmail.com"));
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteContact_GivenValidContactId()
        {
            var contactId = 1;

            Assert.That(await _repository.GetByIdAsync(contactId) != null);

            await _repository.DeleteAsync(contactId);

            Assert.That(await _repository.GetByIdAsync(contactId) == null);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateContact_GivenValidUpdatedContact()
        {
            var contactId = 1;
            var existingContact = await _repository.GetByIdAsync(contactId);

            var newMiddleName = "Frank";
            var newStreetAddress = "8360 High Autumn Road";
            existingContact.Name.Middle = newMiddleName;
            existingContact.Address.Street = newStreetAddress;

            await _repository.UpdateAsync(existingContact);

            var updated = await _repository.GetByIdAsync(existingContact.Id);

            Assert.That(updated.Name.Middle == newMiddleName);
            Assert.That(updated.Address.Street == newStreetAddress);
        }

        [Test]
        public async Task ListAsyncWithQuery_ShouldReturnFilteredContactList_GivenDataInDatabase()
        {
            var result = await _repository.ListAsync(c => c.Name.Last == "Rowe");

            Assert.That(result.Count == 1);
            Assert.That(result.First().Name.Last == "Rowe");
        }

        [Test]
        public async Task ListAsyncWithQuery_ShouldReturnCallList_GivenCallListCriterion()
        {
            var result = await _repository.ListAsync(c => c.Phone.Where(p => p.Type == PhoneType.Home).Any());

            Assert.That(result.All(c => c.Phone.Where(p => p.Type == PhoneType.Home).Any()));
        }
    }
}