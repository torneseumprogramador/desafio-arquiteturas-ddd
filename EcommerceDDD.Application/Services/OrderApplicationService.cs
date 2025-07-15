using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceDDD.Application.DTOs;
using EcommerceDDD.Application.Interfaces;
using EcommerceDDD.Domain.Entities;
using EcommerceDDD.Domain.Interfaces;

namespace EcommerceDDD.Application.Services
{
    public class OrderApplicationService : IOrderApplicationService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderApplicationService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            // Verificar se a pessoa existe
            var person = await _userRepository.GetByIdAsync(dto.PersonId);
            if (person == null)
                throw new InvalidOperationException("Pessoa não encontrada");

            // Criar o pedido
            var order = new Order(dto.PersonId);

            // Adicionar produtos ao pedido
            foreach (var productDto in dto.Products)
            {
                var product = await _productRepository.GetByIdAsync(productDto.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Produto com ID {productDto.ProductId} não encontrado");

                order.AddProduct(product, productDto.Quantity);
                
                // Atualizar estoque do produto
                product.DecreaseStock(productDto.Quantity);
                await _productRepository.UpdateAsync(product);
            }

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return await MapToDto(order, person);
        }

        public async Task<OrderDto> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdWithProductsAsync(id);
            if (order == null)
                throw new InvalidOperationException("Pedido não encontrado");

            var person = await _userRepository.GetByIdAsync(order.PersonId);
            return await MapToDto(order, person);
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var person = await _userRepository.GetByIdAsync(order.PersonId);
                orderDtos.Add(await MapToDto(order, person));
            }

            return orderDtos;
        }

        public async Task<IEnumerable<OrderDto>> GetByPersonAsync(Guid personId)
        {
            var orders = await _orderRepository.GetByPersonAsync(personId);
            var person = await _userRepository.GetByIdAsync(personId);
            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                orderDtos.Add(await MapToDto(order, person));
            }

            return orderDtos;
        }

        public async Task<IEnumerable<OrderDto>> GetByStatusAsync(string status)
        {
            if (!Enum.TryParse<OrderStatus>(status, true, out var orderStatus))
                throw new InvalidOperationException("Status inválido");

            var orders = await _orderRepository.GetByStatusAsync(orderStatus);
            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var person = await _userRepository.GetByIdAsync(order.PersonId);
                orderDtos.Add(await MapToDto(order, person));
            }

            return orderDtos;
        }

        public async Task<OrderDto> ConfirmAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new InvalidOperationException("Pedido não encontrado");

            order.Confirm();

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var person = await _userRepository.GetByIdAsync(order.PersonId);
            return await MapToDto(order, person);
        }

        public async Task<OrderDto> ShipAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new InvalidOperationException("Pedido não encontrado");

            order.Ship();

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var person = await _userRepository.GetByIdAsync(order.PersonId);
            return await MapToDto(order, person);
        }

        public async Task<OrderDto> DeliverAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new InvalidOperationException("Pedido não encontrado");

            order.Deliver();

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var person = await _userRepository.GetByIdAsync(order.PersonId);
            return await MapToDto(order, person);
        }

        public async Task<OrderDto> CancelAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdWithProductsAsync(id);
            if (order == null)
                throw new InvalidOperationException("Pedido não encontrado");

            // Restaurar estoque dos produtos se o pedido ainda não foi entregue
            if (order.Status != OrderStatus.Delivered)
            {
                foreach (var orderProduct in order.OrderProducts)
                {
                    var product = await _productRepository.GetByIdAsync(orderProduct.ProductId);
                    if (product != null)
                    {
                        product.IncreaseStock(orderProduct.Quantity);
                        await _productRepository.UpdateAsync(product);
                    }
                }
            }

            order.Cancel();

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var person = await _userRepository.GetByIdAsync(order.PersonId);
            return await MapToDto(order, person);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!await _orderRepository.ExistsAsync(id))
                throw new InvalidOperationException("Pedido não encontrado");

            await _orderRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _orderRepository.ExistsAsync(id);
        }

        private static async Task<OrderDto> MapToDto(Order order, IPerson person)
        {
            return new OrderDto
            {
                Id = order.Id,
                PersonId = order.PersonId,
                PersonName = person?.Name,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                Products = order.OrderProducts.Select(op => new OrderProductDto
                {
                    Id = op.Id,
                    ProductId = op.ProductId,
                    ProductName = "", // Será preenchido se necessário
                    UnitPrice = op.UnitPrice,
                    Quantity = op.Quantity,
                    TotalPrice = op.TotalPrice
                }).ToList()
            };
        }
    }
} 