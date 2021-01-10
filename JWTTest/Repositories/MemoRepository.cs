using MemoApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MemoApp.Repositories
{
    public interface IMemoRepository
    {
        Task<Memo> CreateAsync(Memo memo);
        Task<Memo> GetByUserIdAsync(int userId);
    }

    public class MemoRepository : IMemoRepository
    {
        private readonly ApplicationContext _context;

        public MemoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Memo> CreateAsync(Memo memo)
        {
            var result = _context.Memos.Add(memo).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Memo> GetByUserIdAsync(int userId)
        {
            return await _context.Memos
                .FirstOrDefaultAsync(x => x.UserLoginId == userId);
        }
    }
}
