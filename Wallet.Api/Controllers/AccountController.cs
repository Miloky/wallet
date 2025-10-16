using Microsoft.AspNetCore.Mvc;

namespace Wallet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController: ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAccount()
    {
        return Created("", new { Id = -100 });
    }
}