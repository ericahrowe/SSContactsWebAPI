using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace SSContacts.WebApi
{
    /// <summary>A lower case naming strategy.</summary>
    public class LowerCaseNamingStrategy : JsonNamingPolicy
    {
        public LowerCaseNamingStrategy() { }

        public override string ConvertName(string name)
        {
           return name.ToLower();
        }
    }
}
