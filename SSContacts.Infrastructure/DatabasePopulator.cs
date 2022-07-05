using SSContacts.Core.Entities;
using SSContacts.Infrastructure.Data;
using SSContacts.SharedKernel;
using System.Collections.Generic;
using System.Linq;

namespace SSContacts.Infrastructure
{
    public static class DatabasePopulator
    {
        public static int PopulateDatabase(IContactRepository todoRepository)
        {
            if (todoRepository.ListAsync().Result.Count() >= 3) return 0;

            todoRepository.AddAsync(new Contact
            {
                Name = new Name("Harold", "Francis", "Gilkey"),
                Address = new Address("8360 High Autumn Row", "Cannon", "Delaware", "19797"),
                Phone = new List<Phone>() { new Phone("302-611-9148", PhoneType.Home), new Phone("302-532-9427", PhoneType.Mobile) },
                Email = "harold.gilkey@yahoo.com"
            }).Wait();
            todoRepository.AddAsync(new Contact
            {
                Name = new Name("Erica", "Hunter", "Rowe"),
                Address = new Address("1187 Howardsville Rd", "Staunton", "Virginia", "24401"),
                Phone = new List<Phone>() { new Phone("434-964-8535", PhoneType.Mobile) },
                Email = "erica.rowe@mindspring.com"
            }).Wait();
            todoRepository.AddAsync(new Contact
            {
                Name = new Name("Maude", "Francine", "Gilkey"),
                Address = new Address("8860 High Autumn Row", "Cannon", "Delaware", "19797"),
                Phone = new List<Phone>() { new Phone("302-611-9899", PhoneType.Home), new Phone("302-532-4321", PhoneType.Mobile) },
                Email = "maude.gilkey@yahoo.com"
            }).Wait();

            return todoRepository.ListAsync().Result.Count;
        }
    }
}