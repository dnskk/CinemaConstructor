using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaConstructor.Database.Repositories
{
    public class UserSessionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserSessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserSession>> GetAllAsync(CancellationToken token)
        {
            return await _context.UserSessions.ToListAsync(token);
        }

        public async Task<UserSession> FindByUserIdAsync(Guid id, CancellationToken token)
        {
            var keys = new object[] { id };
            return await _context.UserSessions.FindAsync(keys, token);
        }

        public async Task<UserSession> UpdateAsync(UserSession userSession, CancellationToken token)
        {
            var currentUserSession = await FindByUserIdAsync(userSession.UserId, token);
            if (currentUserSession == null)
            {
                _context.UserSessions.Add(userSession);
            }
            else
            {
                currentUserSession.CurrentCompanyId = userSession.CurrentCompanyId;
                currentUserSession.CurrentCinemaId = userSession.CurrentCinemaId;
                currentUserSession.CurrentFilmId = userSession.CurrentFilmId;
                currentUserSession.CurrentFilmSessionId = userSession.CurrentFilmSessionId;
            }

            await _context.SaveChangesAsync(token);

            return userSession;
        }
    }
}
