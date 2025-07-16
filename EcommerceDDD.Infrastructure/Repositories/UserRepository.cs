using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcommerceDDD.Domain.Entities;
using EcommerceDDD.Domain.Interfaces;
using EcommerceDDD.Domain.ValueObjects;
using EcommerceDDD.Infrastructure.Data;

namespace EcommerceDDD.Infrastructure.Repositories
{
    public class UserRepository : Repository<Person>, IUserRepository
    {
        public UserRepository(EcommerceDbContext context) : base(context)
        {
        }

        public async Task<IPerson> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IPerson> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IndividualPerson> GetByCPFAsync(CPF cpf)
        {
            return await _context.Set<IndividualPerson>()
                .FirstOrDefaultAsync(p => p.CPF == cpf);
        }

        public async Task<CorporatePerson> GetByCNPJAsync(CNPJ cnpj)
        {
            return await _context.Set<CorporatePerson>()
                .FirstOrDefaultAsync(p => p.CNPJ == cnpj);
        }

        public async Task<IEnumerable<IPerson>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<IndividualPerson>> GetAllIndividualsAsync()
        {
            return await _context.Set<IndividualPerson>().ToListAsync();
        }

        public async Task<IEnumerable<CorporatePerson>> GetAllCorporatesAsync()
        {
            return await _context.Set<CorporatePerson>().ToListAsync();
        }

        public async Task<IPerson> AddAsync(IPerson person)
        {
            if (person is IndividualPerson individualPerson)
            {
                await _context.Set<IndividualPerson>().AddAsync(individualPerson);
            }
            else if (person is CorporatePerson corporatePerson)
            {
                await _context.Set<CorporatePerson>().AddAsync(corporatePerson);
            }

            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<IPerson> UpdateAsync(IPerson person)
        {
            if (person is IndividualPerson individualPerson)
            {
                _context.Set<IndividualPerson>().Update(individualPerson);
            }
            else if (person is CorporatePerson corporatePerson)
            {
                _context.Set<CorporatePerson>().Update(corporatePerson);
            }

            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(p => p.Email == email);
        }

        public async Task<bool> ExistsByCPFAsync(CPF cpf)
        {
            return await _context.Set<IndividualPerson>()
                .AnyAsync(p => p.CPF == cpf);
        }

        public async Task<bool> ExistsByCNPJAsync(CNPJ cnpj)
        {
            return await _context.Set<CorporatePerson>()
                .AnyAsync(p => p.CNPJ == cnpj);
        }
    }
} 