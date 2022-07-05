using SSContacts.SharedKernel;

namespace SSContacts.WebApi.Endpoints.Contacts
{
    public class GetCallListResponse
    {
        public Name Name { get; set; }

        public string Phone { get; set; }
    }
}