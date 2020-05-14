using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaConstructor.Database.Repositories
{
    public class CompanyMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyMemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyMember>> GetAllAsync(CancellationToken token)
        {
            return await _context.CompanyMembers
                .Include(p => p.Company)
                .ToListAsync(token);
        }

        public async Task<CompanyMember> FindByIdAsync(long id, CancellationToken token)
        {
            var keys = new object[] { id };
            return await _context.CompanyMembers.FindAsync(keys, token);
        }

        public async Task<IEnumerable<CompanyMember>> FindByUserIdAsync(string userId, CancellationToken token)
        {
            return await _context.CompanyMembers
                .Where(p => p.UserId== userId)
                .Include(p => p.Company)
                .ToListAsync(token);
        }

        public async Task<CompanyMember> AddAsync(CompanyMember companyMember, CancellationToken token)
        {
            companyMember.Id = 0;

            _context.CompanyMembers.Add(companyMember);
            await _context.SaveChangesAsync(token);

            return companyMember;
        }

        public async Task<CompanyMember> UpdateAsync(CompanyMember companyMember, CancellationToken token)
        {
            _context.CompanyMembers.Update(companyMember);
            await _context.SaveChangesAsync(token);

            return companyMember;
        }
    }
}
