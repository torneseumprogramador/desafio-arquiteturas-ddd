using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcommerceDDD.Domain.Entities;
using EcommerceDDD.Domain.Interfaces;
using EcommerceDDD.Infrastructure.Data;

namespace EcommerceDDD.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(EcommerceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetByStockAsync(int minStock)
        {
            return await _dbSet.Where(p => p.Stock >= minStock).ToListAsync();
        }
    }
} 