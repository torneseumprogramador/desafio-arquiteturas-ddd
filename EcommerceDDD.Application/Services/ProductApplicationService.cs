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
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductApplicationService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product(dto.Name, dto.Description, dto.Price, dto.Stock);
            
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(product);
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new InvalidOperationException("Produto não encontrado");

            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(MapToDto);
        }

        public async Task<IEnumerable<ProductDto>> GetByStockAsync(int minStock)
        {
            var products = await _productRepository.GetByStockAsync(minStock);
            return products.Select(MapToDto);
        }

        public async Task<ProductDto> UpdateAsync(Guid id, CreateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new InvalidOperationException("Produto não encontrado");

            product.UpdateName(dto.Name);
            product.UpdateDescription(dto.Description);
            product.UpdatePrice(dto.Price);
            product.UpdateStock(dto.Stock);

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!await _productRepository.ExistsAsync(id))
                throw new InvalidOperationException("Produto não encontrado");

            await _productRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _productRepository.ExistsAsync(id);
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
} 