using Contacts.Application.DTOs;
using Contacts.Application.Operations.Commands;
using Contacts.Application.Operations.Queries;
using Contacts.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace Contacts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<int> CreateContact([AsParameters] CreateContactDto contactDto)
        {
            var command = new CreateContactCommand(contactDto.FirstName, contactDto.LastName, contactDto.Email);
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResponse<Contact>> GetContacts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 0,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool isAscending = true)
        {
            var query = new GetContactsQuery(pageNumber, pageSize, searchTerm, sortBy, isAscending);
            return await mediator.Send(query);
        }

        [HttpGet("{id:int}")]
        public async Task<Contact> GetContactById(int id)
        {
            var query = new GetContactByIdQuery(id);
            return await mediator.Send(query);
        }

        [HttpPut("{id:int}")]
        public async Task UpdateContact(int id, [AsParameters] UpdateContactDto updateContactDto)
        {
            var command = new UpdateContactCommand(id, updateContactDto.FirstName, updateContactDto.LastName, updateContactDto.Email);
            await mediator.Send(command);
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteContact(int id)
        {
            var command = new DeleteContactCommand(id);
            await mediator.Send(command);
        }

    }
}
