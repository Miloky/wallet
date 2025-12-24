namespace Wallet.Api.Contracts;

public class CreateAccountRequest
{
    // Length: 100
    // Should be unique? It should be unique
    public string Name { get; set; }
}