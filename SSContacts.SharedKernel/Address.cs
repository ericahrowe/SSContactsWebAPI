using Ardalis.GuardClauses;

namespace SSContacts.SharedKernel
{
    public class Address
    {
        public Address(string street, string city, string state, string zip)
        {
            Street = Guard.Against.NullOrEmpty(street, nameof(street));
            City = Guard.Against.NullOrEmpty(city, nameof(city));
            State = Guard.Against.NullOrEmpty(state, nameof(state));
            Zip = Guard.Against.NullOrEmpty(zip, nameof(zip));
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}