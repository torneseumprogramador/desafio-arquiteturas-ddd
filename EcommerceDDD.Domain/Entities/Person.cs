using System;
using EcommerceDDD.Domain.Interfaces;

namespace EcommerceDDD.Domain.Entities
{
    public abstract class Person : IPerson
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        protected Person(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            CreatedAt = DateTime.UtcNow;
        }

        // Construtor para EF Core
        protected Person() { }

        // Métodos comuns
        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Nome não pode ser vazio", nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email não pode ser vazio", nameof(newEmail));

            Email = newEmail;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Senha não pode ser vazia", nameof(newPassword));

            Password = newPassword;
            UpdatedAt = DateTime.UtcNow;
        }

        // Métodos abstratos que devem ser implementados pelas classes filhas
        public abstract string GetDocument();
        public abstract string GetDocumentValue();
        public abstract string GetPersonType();
    }
} 