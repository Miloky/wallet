namespace Wallet.Api;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
}