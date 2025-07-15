using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EcommerceDDD.Infrastructure.Data
{
    public class EcommerceDbContextFactory : IDesignTimeDbContextFactory<EcommerceDbContext>
    {
        public EcommerceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EcommerceDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=EcommerceDDD;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=true;");

            return new EcommerceDbContext(optionsBuilder.Options);
        }
    }
} 