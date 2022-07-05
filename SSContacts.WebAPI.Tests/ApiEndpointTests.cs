using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using SSContacts.WebApi;
using SSContacts.WebApi.ApiModels;
using System.Net;
using System.Threading.Tasks;

namespace SSContacts.WebAPI.Tests
{
    public class ApiEndpointTests
    {
        //In the interests of time, I only wrote tests of this one method.
        [Test]
        public async Task GetById_ShouldReturnContact_GivenValidId()
        {
            var factory = new WebApplicationFactory<Startup>();

            var client = factory.CreateClient();

            var result = await client.GetAsync($"contacts/1");

            var content = await result.Content.ReadAsStringAsync();

            var resultObject = JsonConvert.DeserializeObject<GetContactResponse>(content);

            Assert.That(resultObject.Id == 1);
        }

        [Test]
        public async Task GetById_ShouldReturn404_GivenInvalidId()
        {
            var factory = new WebApplicationFactory<Startup>();

            var client = factory.CreateClient();

            var result = await client.GetAsync($"contacts/999");

            var content = await result.Content.ReadAsStringAsync();

            Assert.That(result.StatusCode == HttpStatusCode.NotFound);
        }
    }
}