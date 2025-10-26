using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController: ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly AccountService _accountService;
    

    public AccountController(ILogger<AccountController> logger, AccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> CreateAccount()
    {
        _logger.LogInformation("Creating account");
        var account = new Account
        {
            Name = "monobank" + new Random().Next(),
            InitialBalance = 0
        };
        await _accountService.CreateAsync(account);
        
        
        return Created("", await _accountService.GetAllAsync());
    }
}