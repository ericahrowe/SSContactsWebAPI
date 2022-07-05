using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SSContacts.Core.Entities;
using SSContacts.Infrastructure.Data;
using SSContacts.WebApi.ApiModels;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace SSContacts.WebAPI.Endpoints.Contacts
{
    public class GetById : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<GetContactResponse>
    {
        private readonly IContactRepository _repository;

        public GetById(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/contacts/{id:int}")]
        [SwaggerOperation(
            Summary = "Gets a single Contact by Id",
            Description = "Gets a single Contact by Id",
            OperationId = "Contacts.GetById")
        ]
        public override async Task<ActionResult<GetContactResponse>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null)
                return NotFound();

            //This is probably where I would introduce a mapper, rather than continuing to do this manually.
            var response = new GetContactResponse()
            {
                Id = contact.Id,
                Name = contact.Name,
                Address = contact.Address,
                Phone = contact.Phone,
                Email = contact.Email
            };

            return Ok(response);
        }
    }
}