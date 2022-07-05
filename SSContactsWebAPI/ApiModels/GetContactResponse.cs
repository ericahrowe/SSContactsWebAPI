using SSContacts.SharedKernel;
using System.Collections.Generic;

namespace SSContacts.WebApi.ApiModels
{
    public class GetContactResponse
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phone { get; set; }
        public string Email { get; set; }
    }
}