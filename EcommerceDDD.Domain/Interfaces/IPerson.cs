using System;

namespace EcommerceDDD.Domain.Interfaces
{
    public interface IPerson
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        string Password { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
        
        string GetDocument();
        string GetDocumentValue();
        string GetPersonType();
    }
} 