using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Data;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories
{
    public class CinemaRepository
    {
        private readonly ApplicationDbContext _context;

        public CinemaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cinema>> GetAllAsync(CancellationToken token)
        {
            return await _context.Cinemas.ToListAsync(token);
        }

        public async Task<Cinema> FindByIdAsync(long id, CancellationToken token)
        {
            var keys = new object[] { id };
            return await _context.Cinemas.FindAsync(keys, token);
        }

        public async Task<IEnumerable<Cinema>> FindByCompanyIdAsync(long companyId, CancellationToken token)
        {
            return await _context.Cinemas
                .Where(p => p.Company.Id == companyId)
                .Include(p => p.Company)
                .ToListAsync(token);
        }

        public async Task<Cinema> AddAsync(Cinema cinema, CancellationToken token)
        {
            cinema.Id = 0;

            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync(token);

            return cinema;
        }

        public async Task<Cinema> UpdateAsync(Cinema cinema, CancellationToken token)
        {
            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync(token);

            return cinema;
        }
    }
}
