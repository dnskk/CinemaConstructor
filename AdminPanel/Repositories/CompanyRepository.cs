﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Data;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories
{
    public class CompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllAsync(CancellationToken token)
        {
            return await _context.Companies.ToListAsync(token);
        }

        public async Task<Company> FindByIdAsync(long id, CancellationToken token)
        {
            var keys = new object[] { id };
            return await _context.Companies.FindAsync(keys, token);
        }

        public async Task<Company> AddAsync(Company company, CancellationToken token)
        {
            company.Id = Guid.NewGuid();

            _context.Companies.Add(company);
            await _context.SaveChangesAsync(token);

            return company;
        }

        public async Task<Company> UpdateAsync(Company company, CancellationToken token)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync(token);

            return company;
        }
    }
}
