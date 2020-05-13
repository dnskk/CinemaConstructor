using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Data;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories
{
    public class HallRepository
    {
        private readonly ApplicationDbContext _context;

        public HallRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hall>> GetAllAsync(CancellationToken token)
        {
            return await _context.Halls.ToListAsync(token);
        }

        public async Task<Hall> FindByIdAsync(long id, CancellationToken token)
        {
            var keys = new object[] { id };
            return await _context.Halls.FindAsync(keys, token);
        }

        public async Task<IEnumerable<Hall>> FindByCinemaIdAsync(long cinemaId, CancellationToken token)
        {
            return await _context.Halls
                .Where(p => p.Cinema.Id == cinemaId)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Hall>> FindByCompanyIdAsync(long companyId, CancellationToken token)
        {
            return await _context.Halls
                .Where(p => p.Cinema.Company.Id == companyId)
                .Include(p => p.Cinema)
                .ToListAsync(token);
        }

        public async Task<Hall> AddAsync(Hall hall, CancellationToken token)
        {
            hall.Id = 0;

            _context.Halls.Add(hall);
            await _context.SaveChangesAsync(token);

            return hall;
        }

        public async Task<Hall> UpdateAsync(Hall hall, CancellationToken token)
        {
            _context.Halls.Update(hall);
            await _context.SaveChangesAsync(token);

            return hall;
        }
    }
}
