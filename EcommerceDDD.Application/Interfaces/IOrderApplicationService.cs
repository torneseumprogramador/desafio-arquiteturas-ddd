using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceDDD.Application.DTOs;

namespace EcommerceDDD.Application.Interfaces
{
    public interface IOrderApplicationService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto dto);
        Task<OrderDto> GetByIdAsync(Guid id);
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<IEnumerable<OrderDto>> GetByPersonAsync(Guid personId);
        Task<IEnumerable<OrderDto>> GetByStatusAsync(string status);
        Task<OrderDto> ConfirmAsync(Guid id);
        Task<OrderDto> ShipAsync(Guid id);
        Task<OrderDto> DeliverAsync(Guid id);
        Task<OrderDto> CancelAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
} 