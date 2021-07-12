namespace Wallet.Api.SmartTable
{
    public class SmartTableResult<T>
    {
        public T Items { get; set; }
        public int TotalRecord { get; set; }
        public int NumberOfPages { get; set; }
    }
}