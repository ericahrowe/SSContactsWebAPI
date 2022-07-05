using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SSContacts.Infrastructure.Data;
using SSContacts.SharedKernel;
using SSContacts.WebApi.ApiModels;
using SSContacts.WebApi.Endpoints.Contacts;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SSContacts.WebAPI.Endpoints.Contacts
{
    public class GetCallList : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<CallListResponse>
    {
        private readonly IContactRepository _repository;

        public GetCallList(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/contacts/call-list")]
        [SwaggerOperation(
            Summary = "Gets a call list of Contacts",
            Description = "Gets a call list of Contacts",
            OperationId = "Contacts.CallList")
        ]
        public override async Task<ActionResult<CallListResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var contacts = await _repository.ListAsync(c => c.Phone.Where(p => p.Type == PhoneType.Home).Any());

            //Eventually, I would like to have the order by done in the Repo, but in the interest of time...
            //Again, I would prefer use of a mapper here.
            var callList = contacts.OrderBy(c => c.Name.Last).ThenBy(c => c.Name.First).Select(contact => new GetCallListResponse()
            {
                Name = contact.Name,
                Phone = contact.Phone.First(p => p.Type == PhoneType.Home).Number,
            });

            return Ok(callList);
        }
    }
}