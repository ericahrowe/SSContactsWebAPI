using Ardalis.GuardClauses;

namespace SSContacts.SharedKernel
{
    public class Phone
    {
        public Phone(string number, PhoneType type)
        {
            //Could put additional validation here.
            Number = Guard.Against.NullOrEmpty(number, nameof(number));
            Type = type;
        }

        public string Number { get; set; }

        public PhoneType Type { get; set; }
    }
}