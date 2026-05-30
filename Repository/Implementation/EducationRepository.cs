using Microsoft.EntityFrameworkCore;
using Project_5_final.Models;
using Project_5_final.Repository.Interface;

namespace Project_5_final.Repository.Implementation
{
    public class EducationRepository: IEducationRepository

    {
        private readonly UserdbContext _context;

        public EducationRepository(UserdbContext context)
        {
            _context = context;
        }

        public async Task<List<Education>> GetEducationByUserIdAsync(int userId)
        {
            return await _context.Educations.Where(e => e.UserId == userId).ToListAsync();
        }
        public async Task<Education?> GetEducationForUserAsync(int userId, string institution, string degree)
        {
            return await _context.Educations
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Institution == institution && e.Degree == degree);
        }
        public async Task<bool> AddEducationAsync(Education education)
        {
            await _context.Educations.AddAsync(education);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEducationAsync(Education education)
        {
            _context.Educations.Update(education);
            return await _context.SaveChangesAsync()>0;
        }
    }
}
