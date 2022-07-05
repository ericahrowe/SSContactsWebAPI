using Ardalis.GuardClauses;

namespace SSContacts.SharedKernel
{
    public class Name
    {
        public Name(string first, string middle, string last)
        {
            
            First = Guard.Against.NullOrEmpty(first, nameof(first));
            Middle = middle;
            Last = Guard.Against.NullOrEmpty(last, nameof(last));
        }

        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }
}