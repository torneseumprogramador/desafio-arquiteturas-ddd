using System;
using EcommerceDDD.Domain.ValueObjects;

namespace EcommerceDDD.Domain.Entities
{
    public class CorporatePerson : Person
    {
        public CNPJ CNPJ { get; private set; }

        public CorporatePerson(string name, string email, string password, CNPJ cnpj) 
            : base(name, email, password)
        {
            CNPJ = cnpj ?? throw new ArgumentNullException(nameof(cnpj));
        }

        // Construtor para EF Core
        private CorporatePerson() { }

        public void UpdateCNPJ(CNPJ newCnpj)
        {
            CNPJ = newCnpj ?? throw new ArgumentNullException(nameof(newCnpj));
            UpdatedAt = DateTime.UtcNow;
        }

        public override string GetDocument()
        {
            return CNPJ?.GetFormatted() ?? string.Empty;
        }

        public override string GetDocumentValue()
        {
            return CNPJ?.Value ?? string.Empty;
        }

        public override string GetPersonType()
        {
            return "Corporate";
        }
    }
} 