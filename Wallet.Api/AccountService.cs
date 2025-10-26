using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wallet.Api.Controllers;

namespace Wallet.Api;

public class AccountService
{
    private readonly IMongoCollection<Account> _accountsCollection;
    private readonly IMongoClient _mongoClient;

    public AccountService(
        IMongoClient mongoClient,
        IOptions<WalletDatabaseSettings> walletDatabaseSettings)
    {
        _mongoClient = mongoClient;
        var mongoDatabase = mongoClient.GetDatabase(
            walletDatabaseSettings.Value.DatabaseName);

        _accountsCollection = mongoDatabase.GetCollection<Account>(
            walletDatabaseSettings.Value.AccountsCollectionName);
    }
    
    public async Task CreateAsync(Account account) => await _accountsCollection.InsertOneAsync(account);
    
    public async Task<List<Account>> GetAllAsync() =>  await _accountsCollection.Find(x => true).ToListAsync();
}