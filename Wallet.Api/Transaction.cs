using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.Api;

public enum TransactionType
{
    Expanse,
    Income,
    Transfer
}

public class Transaction
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public decimal Amount { get; set; }
    
    public TransactionType Type { get; set; }
    
    public DateTime Date { get; set; }
    
    // public string
}