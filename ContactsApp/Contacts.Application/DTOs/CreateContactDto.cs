namespace Contacts.Application.DTOs;

public record CreateContactDto(string FirstName, string LastName, string Email) : ContactDto(FirstName, LastName, Email);
