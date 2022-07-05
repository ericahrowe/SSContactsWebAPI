using NUnit.Framework;
using SSContacts.SharedKernel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SSContacts.WebAPI.Tests
{
    public class Tests
    {
        [Test]
        public void Convert_ShouldConvertWithJsonStringEnumConverter_GivenClassWithEnum()
        {
            var phone = new Phone("540-448-0088", PhoneType.Home);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            var json = "{\"Number\": \"540-448-0088\",\"Type\": \"Home\"}";

            var model = JsonSerializer.Deserialize<Phone>(json, options);

            Assert.IsNotNull(model);
            Assert.That(phone.Number.Equals(model.Number));
            Assert.That(phone.Type.Equals(model.Type));
        }
    }
}