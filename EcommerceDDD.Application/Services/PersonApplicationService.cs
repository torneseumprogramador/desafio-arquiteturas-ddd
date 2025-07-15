using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceDDD.Application.DTOs;
using EcommerceDDD.Application.Interfaces;
using EcommerceDDD.Domain.Entities;
using EcommerceDDD.Domain.Interfaces;
using EcommerceDDD.Domain.ValueObjects;

namespace EcommerceDDD.Application.Services
{
    public class PersonApplicationService : IPersonApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonApplicationService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PersonDto> CreateIndividualAsync(CreateIndividualPersonDto dto)
        {
            // Verificar se email já existe
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
                throw new InvalidOperationException("Email já está em uso");

            // Verificar se CPF já existe
            var cpf = new CPF(dto.CPF);
            if (await _userRepository.ExistsByCPFAsync(cpf))
                throw new InvalidOperationException("CPF já está em uso");

            // Criar pessoa física
            var individualPerson = new IndividualPerson(dto.Name, dto.Email, dto.Password, cpf);
            
            await _userRepository.AddAsync(individualPerson);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(individualPerson);
        }

        public async Task<PersonDto> CreateCorporateAsync(CreateCorporatePersonDto dto)
        {
            // Verificar se email já existe
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
                throw new InvalidOperationException("Email já está em uso");

            // Verificar se CNPJ já existe
            var cnpj = new CNPJ(dto.CNPJ);
            if (await _userRepository.ExistsByCNPJAsync(cnpj))
                throw new InvalidOperationException("CNPJ já está em uso");

            // Criar pessoa jurídica
            var corporatePerson = new CorporatePerson(dto.Name, dto.Email, dto.Password, cnpj);
            
            await _userRepository.AddAsync(corporatePerson);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(corporatePerson);
        }

        public async Task<PersonDto> GetByIdAsync(Guid id)
        {
            var person = await _userRepository.GetByIdAsync(id);
            if (person == null)
                throw new InvalidOperationException("Pessoa não encontrada");

            return MapToDto(person);
        }

        public async Task<PersonDto> GetByEmailAsync(string email)
        {
            var person = await _userRepository.GetByEmailAsync(email);
            if (person == null)
                throw new InvalidOperationException("Pessoa não encontrada");

            return MapToDto(person);
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var persons = await _userRepository.GetAllAsync();
            return persons.Select(MapToDto);
        }

        public async Task<IEnumerable<PersonDto>> GetAllIndividualsAsync()
        {
            var individuals = await _userRepository.GetAllIndividualsAsync();
            return individuals.Select(MapToDto);
        }

        public async Task<IEnumerable<PersonDto>> GetAllCorporatesAsync()
        {
            var corporates = await _userRepository.GetAllCorporatesAsync();
            return corporates.Select(MapToDto);
        }

        public async Task<PersonDto> UpdateAsync(Guid id, CreateIndividualPersonDto dto)
        {
            var person = await _userRepository.GetByIdAsync(id);
            if (person == null)
                throw new InvalidOperationException("Pessoa não encontrada");

            if (person is not IndividualPerson individualPerson)
                throw new InvalidOperationException("Pessoa não é do tipo Individual");

            // Verificar se email já existe (exceto para a própria pessoa)
            var existingPerson = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingPerson != null && existingPerson.Id != id)
                throw new InvalidOperationException("Email já está em uso");

            // Verificar se CPF já existe (exceto para a própria pessoa)
            var cpf = new CPF(dto.CPF);
            var existingIndividual = await _userRepository.GetByCPFAsync(cpf);
            if (existingIndividual != null && existingIndividual.Id != id)
                throw new InvalidOperationException("CPF já está em uso");

            // Atualizar dados
            individualPerson.UpdateName(dto.Name);
            individualPerson.UpdateEmail(dto.Email);
            individualPerson.UpdatePassword(dto.Password);
            individualPerson.UpdateCPF(cpf);

            await _userRepository.UpdateAsync(individualPerson);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(individualPerson);
        }

        public async Task<PersonDto> UpdateAsync(Guid id, CreateCorporatePersonDto dto)
        {
            var person = await _userRepository.GetByIdAsync(id);
            if (person == null)
                throw new InvalidOperationException("Pessoa não encontrada");

            if (person is not CorporatePerson corporatePerson)
                throw new InvalidOperationException("Pessoa não é do tipo Corporate");

            // Verificar se email já existe (exceto para a própria pessoa)
            var existingPerson = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingPerson != null && existingPerson.Id != id)
                throw new InvalidOperationException("Email já está em uso");

            // Verificar se CNPJ já existe (exceto para a própria pessoa)
            var cnpj = new CNPJ(dto.CNPJ);
            var existingCorporate = await _userRepository.GetByCNPJAsync(cnpj);
            if (existingCorporate != null && existingCorporate.Id != id)
                throw new InvalidOperationException("CNPJ já está em uso");

            // Atualizar dados
            corporatePerson.UpdateName(dto.Name);
            corporatePerson.UpdateEmail(dto.Email);
            corporatePerson.UpdatePassword(dto.Password);
            corporatePerson.UpdateCNPJ(cnpj);

            await _userRepository.UpdateAsync(corporatePerson);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(corporatePerson);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!await _userRepository.ExistsAsync(id))
                throw new InvalidOperationException("Pessoa não encontrada");

            await _userRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _userRepository.ExistsAsync(id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _userRepository.ExistsByEmailAsync(email);
        }

        private static PersonDto MapToDto(IPerson person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                PersonType = person.GetPersonType(),
                Document = person.GetDocument(),
                CreatedAt = person.CreatedAt,
                UpdatedAt = person.UpdatedAt
            };
        }
    }
} 