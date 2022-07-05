using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SSContacts.SharedKernel;

namespace SSContacts.WebAPI.Endpoints.Contacts
{
    public class NewContactRequest
    {
        [Required]
        public Name Name { get; set; }

        public List<Phone> Phone { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }
    }
}