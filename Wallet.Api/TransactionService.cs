using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wallet.Api.Controllers;

namespace Wallet.Api;


// TODO: add disposible pattern implementation
public class TransactionService
{
    private readonly IMongoCollection<Transaction> _transactionsCollection;
    private readonly MongoClient _mongoClient;

    public TransactionService(
        IOptions<WalletDatabaseSettings> walletDatabaseSettings, MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        
        var mongoDatabase = _mongoClient.GetDatabase(
            walletDatabaseSettings.Value.DatabaseName);

        _transactionsCollection = mongoDatabase.GetCollection<Transaction>(
            walletDatabaseSettings.Value.TransactionsCollectionName);
    }

    public async Task<List<Transaction>> GetAsync() =>
        await _transactionsCollection.Find(_ => true).ToListAsync();

    public async Task<Transaction?> GetAsync(string id) =>
        await _transactionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Transaction transaction)
    {
        // var database 
        // var accountsCollection = 
        
        using var session = await _mongoClient.StartSessionAsync();
        
        session.StartTransaction();
        await _transactionsCollection.InsertOneAsync(session, transaction);

        await session.CommitTransactionAsync();
        
    }

    public async Task UpdateAsync(string id, Transaction updatedBook) =>
        await _transactionsCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _transactionsCollection.DeleteOneAsync(x => x.Id == id);
}