using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceDDD.Application.DTOs;

namespace EcommerceDDD.Application.Interfaces
{
    public interface IProductApplicationService
    {
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> GetByStockAsync(int minStock);
        Task<ProductDto> UpdateAsync(Guid id, CreateProductDto dto);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
} 