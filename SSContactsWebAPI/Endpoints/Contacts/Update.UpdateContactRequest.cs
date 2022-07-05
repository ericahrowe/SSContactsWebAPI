using SSContacts.SharedKernel;
using System.Collections.Generic;

namespace SSContacts.WebAPI.Endpoints.Contacts
{
    public class UpdateContactRequest
    {
        public int Id { get; set; }
        public Name Name { get; set; }

        public List<Phone> Phone { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }
    }
}