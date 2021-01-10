using MemoApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MemoApp.Repositories
{
    public interface IUserRepository
    {
        Task<UserLogin> Create(UserLogin user);
        Task<UserLogin> Get(int id);
        Task<UserLogin> GetByUsername(string username);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserLogin> Create(UserLogin user)
        {
            var result = _context.UserLogins.Add(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserLogin> Get(int id)
        {
            return await _context.UserLogins
                .Include(x => x.UserType)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserLogin> GetByUsername(string username)
        {
            return await _context.UserLogins
                .Include(x => x.UserType)
                .FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
