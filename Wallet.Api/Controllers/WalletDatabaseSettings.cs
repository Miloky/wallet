namespace Wallet.Api.Controllers;

public class WalletDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string TransactionsCollectionName { get; set; } = null!;

    public string AccountsCollectionName { get; set; } = null!;
}