using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace Wallet.Api.Controllers;

// Features

// TODO: record, struct??
// TODO: FluentValidation
public class CreateTransactionRequest
{
    // transafers are currently not supported
    public TransactionType TransactionType { get; set; }
    // TODO: This should be changed to decimal, having problem with firebase deserializer
    public double Amount { get; set; }
    public string CurrencyCode { get; set; } = "UAH"; // at this moment we only support UAH

    public string AccountId { get; set; }
    public string Category { get; set; }
    public string[] Labels { get; set; }
    public DateTime TransactionDate { get; set; }


    // Other details
    public string Note { get; set; }

    // TODO: Figure out about this fields
    //public string Payer { get; set; }
    //public string PaymentType { get; set; }
    //public string PaymentStatus { get; set; }
}

public class IdObjectResponse
{
    public required string Id { get; set; }
}

public class TransactionData
{
    public string Id { get; set; }
    public string AccountId { get; set; }
    public double Amount { get; set; }
    public string CurrencyCode { get; set; } = "UAH"; // at this moment we only support UAH
    public TransactionType TransactionType { get; set; }
    public string Category { get; set; }
    public string[] Labels { get; set; }
    public DateTime TransactionDate { get; set; }

    // Other details
    public string Note { get; set; }
}

public class GetTransactionsResponse
{
    public IEnumerable<TransactionData> Data { get; set; }
}


// TODO: Think about naming it "RecordController"
// TODO: Add logs
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private const string ProjectId = "wallet-miloky";

    public TransactionsController(ILogger<TransactionsController> logger)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsAsync()
    {
        FirestoreDb db = await FirestoreDb.CreateAsync(ProjectId);
        var snapshot = await db.Collection(TransactionCollectionConstants.CollectionName).GetSnapshotAsync(); // Does this retrieve all data?


        var transactions = new List<TransactionData>(snapshot.Documents.Count);
        foreach (var document in snapshot.Documents)
        {
            var data = new TransactionData
            {
                Id = document.Id,
                AccountId = GetValueOrDefault<string>(document, TransactionCollectionConstants.AccountId, null!),
                CurrencyCode = GetValueOrDefault<string>(document, TransactionCollectionConstants.CurrencyCode, null!),
                Amount = GetValueOrDefault<double>(document, TransactionCollectionConstants.Amount, 0),
                TransactionDate = GetValueOrDefault<DateTime>(document, TransactionCollectionConstants.TransactionDate, DateTime.MinValue),
                Labels = GetValueOrDefault<string[]>(document, TransactionCollectionConstants.Labels, []),
                TransactionType = GetValueOrDefault<TransactionType>(document, TransactionCollectionConstants.TransactionType, TransactionType.Expanse),
                Category = GetValueOrDefault<string>(document, TransactionCollectionConstants.Category, null!),
                Note = GetValueOrDefault<string>(document, TransactionCollectionConstants.Note, null!)
            };
            transactions.Add(data);
        }

        return Ok(transactions);
    }

    private static T GetValueOrDefault<T>(DocumentSnapshot d, string path, T defaultValue) =>
        d.TryGetValue<T>(path, out var val) ? val : defaultValue;


    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequest request)
    {
        FirestoreDb db = await FirestoreDb.CreateAsync(ProjectId);
        var transaction = new Dictionary<string, object>
        {
            { TransactionCollectionConstants.AccountId, request.AccountId },
            { TransactionCollectionConstants.Amount, request.Amount },
            { TransactionCollectionConstants.TransactionType, request.TransactionType },
            { TransactionCollectionConstants.CurrencyCode, request.CurrencyCode },
            { TransactionCollectionConstants.Category, request.Category },
            { TransactionCollectionConstants.Labels, request.Labels },
            { TransactionCollectionConstants.Note, request.Note },
            { TransactionCollectionConstants.TransactionDate, request.TransactionDate.ToUniversalTime() }
        };

        DocumentReference addedDocRef = await db.Collection(TransactionCollectionConstants.CollectionName).AddAsync(transaction);
        return Created("", new IdObjectResponse { Id = addedDocRef.Id });
    }

    private static class TransactionCollectionConstants
    {
        public const string CollectionName = "transactions";

        public const string AccountId = "accountId";
        public const string Amount = "amount";
        public const string TransactionType = "transactionType";
        public const string Category = "category";
        public const string Labels = "labels";
        public const string Note = "note";
        public const string TransactionDate = "transactionDate";
        public const string CurrencyCode = "currencyCode";
    }
}