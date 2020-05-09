using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Data;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories
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
                _context.UserSessions.Update(userSession);
            }

            await _context.SaveChangesAsync(token);

            return userSession;
        }
    }
}
