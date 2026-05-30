using Microsoft.EntityFrameworkCore;
using Project_5_final.DTO;
using Project_5_final.Models;
using Project_5_final.Repository.Interface;


namespace Project_5_final.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserdbContext _context;

        public UserRepository(UserdbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();  
             return user;
            
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync()>0;
        }


        public async  Task<User> GetUserByIdAsync(int Userid)
        {
            return await _context.Users.FindAsync(Userid);
        }

        public async Task<List<User>> GetUserDataAsync(int id)
        {
            try
            {
                var res = await _context.Users.Include(x=>x.Addresses).Include(x=>x.Contacts).Include(x=>x.Educations).Include(x=>x.Employments).Where(x=>x.UserId==id).ToListAsync();
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                return null;
            }
        }
        public async Task<bool> UpdatePasswordAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

       
        public async Task InvalidateTokenAsync(string token, int userId, DateTime expiration)
        {
            var invalidToken = new Invalidtoken
            {
                Token = token,
                Expiration = expiration,
                UserId = userId
            };
            _context.Invalidtokens.Add(invalidToken);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsTokenInvalidatedAsync(string token)
         {
             return await _context.Invalidtokens.AnyAsync(it => it.Token == token && it.Expiration > DateTime.UtcNow);
         }
       public async Task<bool> IsTokenValidAsync(string token)

        {
            var invalidated = await _context.Invalidtokens
                .AnyAsync(t => t.Token == token && t.Expiration > DateTime.UtcNow);
            return !invalidated;

        }
    }
}
