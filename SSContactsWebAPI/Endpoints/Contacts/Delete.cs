using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SSContacts.Core.Entities;
using SSContacts.Infrastructure.Data;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace SSContacts.WebAPI.Endpoints.Contacts
{
    public class Delete : EndpointBaseAsync
        .WithRequest<int>
        .WithoutResult
    {
        private readonly IContactRepository _repository;

        public Delete(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete("/contacts/{id:int}")]
        [SwaggerOperation(
            Summary = "Deletes a Contact by Id",
            Description = "Deletes a Contact by Id",
            OperationId = "Contacts.Delete")
        ]
        public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var contactToDelete = await _repository.GetByIdAsync(id);

            if (contactToDelete == null) return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}