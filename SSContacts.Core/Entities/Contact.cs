using SSContacts.SharedKernel;
using SSContacts.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace SSContacts.Core.Entities
{
    public class Contact : BaseEntity, IAggregateRoot

    {
        public Name Name { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phone { get; set; }
        public string Email { get; set; }
    }
}