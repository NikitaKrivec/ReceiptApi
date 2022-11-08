namespace ReceiptApi.Core.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<Item> ReceiptItems { get; set; }
    }
}
