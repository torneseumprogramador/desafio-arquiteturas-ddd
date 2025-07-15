using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceDDD.Domain.Entities;

namespace EcommerceDDD.Domain.Entities
{
    public enum OrderStatus
    {
        Pending = 1,
        Confirmed = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5
    }

    public class Order
    {
        public Guid Id { get; private set; }
        public Guid PersonId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation properties
        public Person Person { get; private set; }
        public ICollection<OrderProduct> OrderProducts { get; private set; }

        // Construtor para criação
        public Order(Guid personId)
        {
            Id = Guid.NewGuid();
            PersonId = personId;
            Status = OrderStatus.Pending;
            TotalAmount = 0;
            CreatedAt = DateTime.UtcNow;
            OrderProducts = new List<OrderProduct>();
        }

        // Construtor para EF Core
        private Order()
        {
            OrderProducts = new List<OrderProduct>();
        }

        // Métodos de domínio
        public void AddProduct(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));

            if (!product.HasStock(quantity))
                throw new InvalidOperationException("Estoque insuficiente para o produto");

            var existingOrderProduct = OrderProducts.FirstOrDefault(op => op.ProductId == product.Id);
            
            if (existingOrderProduct != null)
            {
                existingOrderProduct.UpdateQuantity(existingOrderProduct.Quantity + quantity);
            }
            else
            {
                var orderProduct = new OrderProduct(Id, product.Id, product.Price, quantity);
                OrderProducts.Add(orderProduct);
            }

            RecalculateTotal();
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveProduct(Guid productId)
        {
            var orderProduct = OrderProducts.FirstOrDefault(op => op.ProductId == productId);
            if (orderProduct != null)
            {
                OrderProducts.Remove(orderProduct);
                RecalculateTotal();
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void UpdateProductQuantity(Guid productId, int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(newQuantity));

            var orderProduct = OrderProducts.FirstOrDefault(op => op.ProductId == productId);
            if (orderProduct != null)
            {
                orderProduct.UpdateQuantity(newQuantity);
                RecalculateTotal();
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void Confirm()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Apenas pedidos pendentes podem ser confirmados");

            if (!OrderProducts.Any())
                throw new InvalidOperationException("Pedido deve ter pelo menos um produto");

            Status = OrderStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Ship()
        {
            if (Status != OrderStatus.Confirmed)
                throw new InvalidOperationException("Apenas pedidos confirmados podem ser enviados");

            Status = OrderStatus.Shipped;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deliver()
        {
            if (Status != OrderStatus.Shipped)
                throw new InvalidOperationException("Apenas pedidos enviados podem ser entregues");

            Status = OrderStatus.Delivered;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Pedidos entregues não podem ser cancelados");

            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        private void RecalculateTotal()
        {
            TotalAmount = OrderProducts.Sum(op => op.TotalPrice);
        }
    }
} 