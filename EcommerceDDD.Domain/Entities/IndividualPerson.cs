using System;
using EcommerceDDD.Domain.ValueObjects;

namespace EcommerceDDD.Domain.Entities
{
    public class IndividualPerson : Person
    {
        public CPF CPF { get; private set; }

        public IndividualPerson(string name, string email, string password, CPF cpf) 
            : base(name, email, password)
        {
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
        }

        // Construtor para EF Core
        private IndividualPerson() { }

        public void UpdateCPF(CPF newCpf)
        {
            CPF = newCpf ?? throw new ArgumentNullException(nameof(newCpf));
            UpdatedAt = DateTime.UtcNow;
        }

        public override string GetDocument()
        {
            return CPF?.GetFormatted() ?? string.Empty;
        }

        public override string GetDocumentValue()
        {
            return CPF?.Value ?? string.Empty;
        }

        public override string GetPersonType()
        {
            return "Individual";
        }
    }
} 