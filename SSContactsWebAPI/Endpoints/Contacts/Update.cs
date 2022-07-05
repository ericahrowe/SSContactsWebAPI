using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SSContacts.Core.Entities;
using SSContacts.Infrastructure.Data;
using SSContacts.WebApi.ApiModels;
using SSContacts.WebAPI.Endpoints.Contacts;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace SSContacts.WebApi.Endpoints.Contacts
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateContactRequest>
        .WithActionResult<UpdateContactResponse>
    {
        private readonly IContactRepository _repository;

        public Update(IContactRepository repository)
        {
            _repository = repository;
        }

        //Because Ardalis doesn't support both path parameters and body parameters, the body actually contains
        //the Id. In the past I wrote some overrides to the Ardalis package to allow this, because we wanted to
        //use the "REST-appropriate" syntax. In this case, in the interest of time, I chose to just put the Id on the UpdateContactRequest.
        [HttpPut("/contacts/{id:int}")]
        [SwaggerOperation(
            Summary = "Updates a Contact",
            Description = "Updates a Contact",
            OperationId = "Contacts.Update")
        ]
        public override async Task<ActionResult<UpdateContactResponse>> HandleAsync(UpdateContactRequest request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id);

            if (contact == null)
                return NotFound();

            contact.Address = request.Address;
            contact.Name = request.Name;
            contact.Email = request.Email;
            contact.Phone = request.Phone;

            await _repository.UpdateAsync(contact);

            return Ok();
        }
    }
}