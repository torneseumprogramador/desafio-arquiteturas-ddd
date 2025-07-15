using System;

namespace EcommerceDDD.Domain.Entities
{
    public class OrderProduct
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }

        // Navigation properties
        public Order Order { get; private set; }
        public Product Product { get; private set; }

        // Construtor para criação
        public OrderProduct(Guid orderId, Guid productId, decimal unitPrice, int quantity)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            
            if (unitPrice <= 0)
                throw new ArgumentException("Preço unitário deve ser maior que zero", nameof(unitPrice));
            UnitPrice = unitPrice;

            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
            Quantity = quantity;

            CalculateTotal();
        }

        // Construtor para EF Core
        private OrderProduct() { }

        // Métodos de domínio
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(newQuantity));

            Quantity = newQuantity;
            CalculateTotal();
        }

        public void UpdateUnitPrice(decimal newUnitPrice)
        {
            if (newUnitPrice <= 0)
                throw new ArgumentException("Preço unitário deve ser maior que zero", nameof(newUnitPrice));

            UnitPrice = newUnitPrice;
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalPrice = UnitPrice * Quantity;
        }
    }
} 