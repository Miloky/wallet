using Wallet.Api.Controllers;

namespace Wallet.Api.SmartTable
{
    public class SmartTableParams
    {
        public Pagination Pagination { get; set; }
        public Sort Sort { get; set; }
        public Search Search { get; set; }
    }
}