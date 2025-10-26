using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.Api;

public class Account
{
    [BsonId] // TODO: find out more about this attribute
    [BsonRepresentation(BsonType.ObjectId)] // TODO: find out more about this attribute
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Currency { get; set; }
    
    public decimal InitialBalance { get; set; }
    
    public string Color { get; set; }
}