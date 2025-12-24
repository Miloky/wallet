using Microsoft.AspNetCore.Mvc;

namespace Wallet.Api.Controllers;

// Features


[ApiController]
[Route("api/[controller]")]
// TODO: Think about naming it "RecordController"
public class TransactionsController : ControllerBase
{
    private readonly TransactionService _transactionService;
    private readonly ILogger<TransactionsController> _logger;

    public TransactionsController(TransactionService transactionService, ILogger<TransactionsController> logger)
    {
        _transactionService = transactionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<object> GetTransactionsAsync()
    {
        // HttpContext.TraceIdentifier
        return Ok();
    }
}