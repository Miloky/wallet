using System;

namespace Wallet.Api.Data
{
    public class Account: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}