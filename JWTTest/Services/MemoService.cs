using MemoApp.Contracts;
using MemoApp.Models;
using MemoApp.Repositories;
using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace MemoApp.Services
{
    public interface IMemoService
    {
        Task<MemoDto> CreateAsync(MemoDto memo);
        Task<MemoDto> GetByUserIdAsync(int userId);
    }

    public class MemoService : IMemoService
    {
        private readonly IMemoRepository _memoRepository;

        public MemoService(IMemoRepository memoRepository)
        {
            _memoRepository = memoRepository;
        }

        public async Task<MemoDto> CreateAsync(MemoDto memo)
        {
            Validate(memo);

            var memoEntity = new Memo()
            {
                Id = 0,
                Title = memo.Title,
                Content = memo.Content,
                UserLoginId = memo.UserLoginId
            };

            await _memoRepository.CreateAsync(memoEntity);

            return memo;
        }

        public async Task<MemoDto> GetByUserIdAsync(int userId)
        {
            var entity = await _memoRepository.GetByUserIdAsync(userId);

            return entity != null 
                ? new MemoDto()
                    {
                        Title = entity.Title,
                        Content = entity.Content,
                        UserLoginId = entity.UserLoginId
                    } 
                : null;
        }

        private void Validate(MemoDto memo)
        {
            try
            {
                byte[] decoded = Convert.FromBase64String(memo.Content);
                using var fs = new FileStream(memo.Title + ".wav", FileMode.Create, FileAccess.Write);
                fs.Write(decoded, 0, decoded.Length);
            }
            catch
            {
                throw new InvalidDataException("Memo content could not be converted to .wav");
            }
        }
    }
}
