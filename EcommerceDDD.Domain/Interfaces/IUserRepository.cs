using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceDDD.Domain.Entities;
using EcommerceDDD.Domain.ValueObjects;

namespace EcommerceDDD.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IPerson> GetByIdAsync(Guid id);
        Task<IPerson> GetByEmailAsync(string email);
        Task<IndividualPerson> GetByCPFAsync(CPF cpf);
        Task<CorporatePerson> GetByCNPJAsync(CNPJ cnpj);
        Task<IEnumerable<IPerson>> GetAllAsync();
        Task<IEnumerable<IndividualPerson>> GetAllIndividualsAsync();
        Task<IEnumerable<CorporatePerson>> GetAllCorporatesAsync();
        Task<IPerson> AddAsync(IPerson person);
        Task<IPerson> UpdateAsync(IPerson person);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByCPFAsync(CPF cpf);
        Task<bool> ExistsByCNPJAsync(CNPJ cnpj);
    }
} 