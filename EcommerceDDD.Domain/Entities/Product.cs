using System;

namespace EcommerceDDD.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Construtor para criação
        public Product(string name, string description, decimal price, int stock)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            
            if (price <= 0)
                throw new ArgumentException("Preço deve ser maior que zero", nameof(price));
            Price = price;

            if (stock < 0)
                throw new ArgumentException("Estoque não pode ser negativo", nameof(stock));
            Stock = stock;

            CreatedAt = DateTime.UtcNow;
        }

        // Construtor para EF Core
        private Product() { }

        // Métodos de domínio
        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Nome não pode ser vazio", nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Preço deve ser maior que zero", nameof(newPrice));

            Price = newPrice;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStock(int newStock)
        {
            if (newStock < 0)
                throw new ArgumentException("Estoque não pode ser negativo", nameof(newStock));

            Stock = newStock;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));

            if (Stock < quantity)
                throw new InvalidOperationException("Estoque insuficiente");

            Stock -= quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));

            Stock += quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool HasStock(int quantity)
        {
            return Stock >= quantity;
        }
    }
} 