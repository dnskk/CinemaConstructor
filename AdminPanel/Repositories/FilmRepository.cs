using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Data;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories
{
    public class FilmRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Film>> GetAllAsync(CancellationToken token)
        {
            return await _context.Films.ToListAsync(token);
        }

        public async Task<Film> FindByIdAsync(long id, CancellationToken token)
        {
            var keys = new object[] { id };
            return await _context.Films.FindAsync(keys, token);
        }

        public async Task<IEnumerable<Film>> FindByCompanyIdAsync(long companyId, CancellationToken token)
        {
            return await _context.Films
                .Where(p => p.Company.Id == companyId)
                .ToListAsync(token);
        }

        public async Task<Film> AddAsync(Film film, CancellationToken token)
        {
            film.Id = 0;

            _context.Films.Add(film);
            await _context.SaveChangesAsync(token);

            return film;
        }

        public async Task<Film> UpdateAsync(Film film, CancellationToken token)
        {
            _context.Films.Update(film);
            await _context.SaveChangesAsync(token);

            return film;
        }
    }
}
