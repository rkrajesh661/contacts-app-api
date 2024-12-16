using Contacts.Domain.Models;

namespace Contacts.Application.Contract;

public interface IContactService
{
    Task<List<Contact>> GetAllContacts();

    Task CreateContact(Contact contact);

    Task UpdateContact(Contact contact);

    Task DeleteContact(int id);

    Task<int> GetNextContactId();
}
