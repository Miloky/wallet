using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Contracts;

namespace Wallet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController: ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly AccountService _accountService;
    
    public AccountsController(ILogger<AccountsController> logger, AccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        _logger.LogInformation("Creating account");
        var account = new Account
        {
            Name = request.Name,
            InitialBalance = 0
        };
        await _accountService.CreateAsync(account);
        return Created("", await _accountService.GetAllAsync());
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountsAsync()
    {
        _logger.LogInformation("Getting accounts");
        var accounts = await _accountService.GetAllAsync();
        _logger.LogInformation("Got accounts");
        
        return Ok(accounts);
    }
}