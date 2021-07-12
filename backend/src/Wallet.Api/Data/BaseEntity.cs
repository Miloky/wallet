using System;

namespace Wallet.Api.Data
{
    public abstract class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public Contact CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Contact ModifiedBy { get; set; }
    }
}