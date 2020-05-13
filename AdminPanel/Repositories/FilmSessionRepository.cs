using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Data;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories
{
    public class FilmSessionRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmSessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FilmSession>> GetAllAsync(CancellationToken token)
        {
            return await _context.FilmSessions.ToListAsync(token);
        }

        public async Task<FilmSession> FindByIdAsync(long id, CancellationToken token)
        {
            var keys = new object[] { id };
            return await _context.FilmSessions.FindAsync(keys, token);
        }

        public async Task<IEnumerable<FilmSession>> FindByCompanyIdAsync(long companyId, CancellationToken token)
        {
            return await _context.FilmSessions
                .Where(p => p.Film.Company.Id == companyId)
                .Include(p => p.Film)
                .ThenInclude(p => p.Company)
                .Include(p =>p.Hall)
                .ThenInclude(p => p.Cinema)
                .ToListAsync(token);
        }

        public async Task<FilmSession> AddAsync(FilmSession filmSession, CancellationToken token)
        {
            filmSession.Id = 0;

            _context.FilmSessions.Add(filmSession);
            await _context.SaveChangesAsync(token);

            return filmSession;
        }

        public async Task<FilmSession> UpdateAsync(FilmSession filmSession, CancellationToken token)
        {
            _context.FilmSessions.Update(filmSession);
            await _context.SaveChangesAsync(token);

            return filmSession;
        }
    }
}
