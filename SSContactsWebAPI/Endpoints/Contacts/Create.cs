using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SSContacts.Core.Entities;
using SSContacts.Infrastructure.Data;
using SSContacts.SharedKernel;
using SSContacts.WebApi.ApiModels;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SSContacts.WebAPI.Endpoints.Contacts
{
    public class Create : EndpointBaseAsync
        .WithRequest<NewContactRequest>
        .WithActionResult<CreateContactResponse>
    {
        private readonly IContactRepository _repository;

        public Create(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("/contacts")]
        [SwaggerOperation(
            Summary = "Creates a new Contact",
            Description = "Creates a new Contact",
            OperationId = "Contacts.Create")
        ]
        public override async Task<ActionResult<CreateContactResponse>> HandleAsync(NewContactRequest request, CancellationToken cancellationToken)
        {
            //For ease of initial implementation, only a single phone type is supported.
            var item = new Contact
            {
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email
            };

            var createdItem = await _repository.AddAsync(item);

            return Ok(createdItem);
        }
    }
}