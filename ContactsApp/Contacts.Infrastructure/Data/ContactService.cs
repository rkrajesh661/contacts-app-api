using Contacts.Application.Contract;
using Contacts.Domain.Models;
using System.Text.Json;

namespace Contacts.Infrastructure.Data
{
    public class ContactService : IContactService
    {
        private readonly string _filePath = "contacts.json";

        public async Task<List<Contact>> GetAllContacts()
        {
            if (!File.Exists(_filePath)) return [];
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? [];
        }

        public async Task CreateContact(Contact contact)
        {
            var contacts = await GetAllContacts();
            contacts.Add(contact);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
        }

        public async Task UpdateContact(Contact contact)
        {
            var contacts = await GetAllContacts();
            var index = contacts.FindIndex(c => c.Id == contact.Id);
            if (index >= 0)
            {
                contacts[index] = contact;
                File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
            }
        }

        public async Task DeleteContact(int id)
        {
            var contacts = await GetAllContacts();
            contacts.RemoveAll(c => c.Id == id);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
        }

        public async Task<int> GetNextContactId()
        {
            var contacts = await GetAllContacts();
            return contacts.Count == 0 ? 1 : contacts.Max(c => c.Id) + 1;
        }
    }
}
