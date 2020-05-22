using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaConstructor.Database.Repositories
{
    public class TicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync(CancellationToken token)
        {
            return await _context.Tickets.ToListAsync(token);
        }

        public async Task<Ticket> FindByIdAsync(long id, CancellationToken token)
        {
            var keys = new object[] { id };
            return await _context.Tickets.FindAsync(keys, token);
        }

        public async Task<IEnumerable<Ticket>> FindByFilmSessionIdAsync(long filmsSessionId, CancellationToken token)
        {
            return await _context.Tickets
                .Where(p => p.FilmSession.Id == filmsSessionId)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Ticket>> FindByBookingIdAsync(Guid bookingId, CancellationToken token)
        {
            return await _context.Tickets
                .Where(p => p.BookingId == bookingId)
                .Include(p => p.FilmSession)
                .ToListAsync(token);
        }

        public async Task<Ticket> AddAsync(Ticket ticket, CancellationToken token)
        {
            ticket.Id = 0;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(token);

            return ticket;
        }

        public async Task<Ticket> UpdateAsync(Ticket ticket, CancellationToken token)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync(token);

            return ticket;
        }
    }
}
