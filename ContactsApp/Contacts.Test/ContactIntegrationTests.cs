using Contacts.Application.DTOs;
using Contacts.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Contacts.Test
{
    [TestFixture]
    public class ContactIntegrationTests
    {
        private HttpClient httpClient;
        private WebApplicationFactory<Contacts.Api.Controllers.ContactsController> factory;

        [SetUp]
        public void SetUp()
        {
            factory = new WebApplicationFactory<Contacts.Api.Controllers.ContactsController>();
            httpClient = factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            httpClient.Dispose();
            factory.Dispose();
        }

        [Test]
        public async Task CreateContact_InvalidModel_ReturnsBadRequest()
        {
            var invalidContact = new CreateContactDto("Test", "Test", "test"); // Invalid email

            var response = await httpClient.PostAsJsonAsync("api/contacts", invalidContact);

            // Assert response is BadRequest due to invalid model
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task CreateContact_ValidModel_ReturnsCreatedResponse()
        {
            var validContact = new CreateContactDto("Test", "test", "test@test.com");

            var response = await httpClient.PostAsJsonAsync("api/contacts", validContact);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var createdContactId = await response.Content.ReadAsStringAsync();
            createdContactId.Should().NotBeNullOrEmpty();

            var getContacts = await httpClient.GetAsync("api/contacts");

            getContacts.StatusCode.Should().Be(HttpStatusCode.OK);

            var contacts = await getContacts.Content.ReadFromJsonAsync<IEnumerable<Contact>>();
            contacts.Should().NotBeEmpty();

            var contactId = Convert.ToInt32(createdContactId);
            response = await httpClient.GetAsync($"api/contacts/{contactId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contact = await response.Content.ReadFromJsonAsync<Contact>();
            contact.Should().NotBeNull();
            contact!.Id.Should().Be(contactId);

            var updateContactDto = new UpdateContactDto("Test", "Test1", "test@test.com");

            response = await httpClient.PutAsJsonAsync($"api/contacts/{contactId}", updateContactDto);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            contact = await response.Content.ReadFromJsonAsync<Contact>();
            contact.Should().NotBeNull();
            contact!.LastName.Should().Be("Test1");

            response = await httpClient.DeleteAsync($"api/contacts/{contactId}");

            // Assert response is NoContent (204)
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task GetContactById_InvalidId_ReturnsNotFound()
        {
            var response = await httpClient.GetAsync("api/contacts/99999"); // Assuming 99999 doesn't exist

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task UpdateContact_InvalidId_ReturnsNotFound()
        {
            var updateContactDto = new UpdateContactDto("Test", "Test", "test@test.com");

            var response = await httpClient.PutAsJsonAsync("api/contacts/999", updateContactDto); // Assuming 999 doesn't exist

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DeleteContact_InvalidId_ReturnsNotFound()
        {
            var response = await httpClient.DeleteAsync("api/contacts/999"); // Assuming 999 doesn't exist

            // Assert response is NotFound (404)
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
