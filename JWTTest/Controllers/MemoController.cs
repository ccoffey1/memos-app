using MemoApp.Contracts;
using MemoApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService _memoService;

        public MemoController(IMemoService memoService)
        {
            _memoService = memoService;
        }

        [HttpPost]
        [Authorize]
        [Route("save")]
        public async Task<ActionResult> SaveMemo(MemoDto memo)
        {
            // pull userid from JWT
            memo.UserLoginId = int.Parse(((ClaimsIdentity)HttpContext.User.Identity)
                .FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _memoService.CreateAsync(memo);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public async Task<ActionResult> GetByUserId()
        {
            // pull userid from JWT
            int userId = int.Parse(((ClaimsIdentity)HttpContext.User.Identity)
                .FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _memoService.GetByUserIdAsync(userId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
