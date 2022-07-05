using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SSContacts.Infrastructure.Data;
using SSContacts.WebApi.ApiModels;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SSContacts.WebAPI.Endpoints.Contacts
{
    public class List : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<GetContactResponse>>
    {
        private readonly IContactRepository _repository;

        public List(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/contacts")]
        [SwaggerOperation(
            Summary = "Gets a list of all Contacts",
            Description = "Gets a list of all Contacts",
            OperationId = "Contacts.List")
        ]
        public override async Task<ActionResult<List<GetContactResponse>>> HandleAsync(CancellationToken cancellationToken)
        {
            var contacts = await _repository.ListAsync();
            //Again, I would prefer use of a mapper here.
            var items = contacts.Select(contact => new GetContactResponse()
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Address = contact.Address,
                    Phone = contact.Phone,
                    Email = contact.Email
                });

            return Ok(items);
        }
    }
}