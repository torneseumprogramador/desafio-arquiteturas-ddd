using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceDDD.Application.DTOs;

namespace EcommerceDDD.Application.Interfaces
{
    public interface IPersonApplicationService
    {
        Task<PersonDto> CreateIndividualAsync(CreateIndividualPersonDto dto);
        Task<PersonDto> CreateCorporateAsync(CreateCorporatePersonDto dto);
        Task<PersonDto> GetByIdAsync(Guid id);
        Task<PersonDto> GetByEmailAsync(string email);
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task<IEnumerable<PersonDto>> GetAllIndividualsAsync();
        Task<IEnumerable<PersonDto>> GetAllCorporatesAsync();
        Task<PersonDto> UpdateAsync(Guid id, CreateIndividualPersonDto dto);
        Task<PersonDto> UpdateAsync(Guid id, CreateCorporatePersonDto dto);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
    }
} 