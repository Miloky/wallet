using System;

namespace Wallet.Api.Data
{
    public class Record
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public RecordType Type { get; set; }
        public DateTime DateTime { get; set; }
        public Account From { get; set; }
        public Account To { get; set; }

        // Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // TODO: Add file attachment
        public string Note { get; set; }

        // Location
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
    }
}