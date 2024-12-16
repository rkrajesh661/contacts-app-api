namespace Contacts.Application.DTOs;

public record UpdateContactDto(string FirstName, string LastName, string Email) : ContactDto(FirstName, LastName, Email);
