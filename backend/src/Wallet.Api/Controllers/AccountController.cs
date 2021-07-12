using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallet.Api.Data;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return Ok(accounts);
        }
    }

    [ApiController]
    [Route("api/records")]
    public class RecordController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public RecordController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var records = await _applicationDbContext.Records.ToListAsync();
            return Ok(records);
        }

        [HttpPost]
        public async Task<IActionResult> Index(RecordCreateModel model, CancellationToken token)
        {
            var record = new Record
            {
                Type = model.Type,
                Note = model.Note
            };

            await _applicationDbContext.AddAsync(record, token);
            await _applicationDbContext.SaveChangesAsync(token);

            return Ok(record.Id);
        }

        [HttpGet]
        [Route("/api/balance/{id}")]
        public async Task<IActionResult> Balance(Guid id)
        {
            var account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(new { account.Id, account.Name, account.Balance });
        }
    }

    public class RecordCreateModel
    {
        public decimal Amount { get; set; }
        public RecordType Type { get; set; }
        public string Note { get; set; }
    }
}