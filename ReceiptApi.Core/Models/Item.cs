using System.Text.Json.Serialization;

namespace ReceiptApi.Core.Models
{
    public class Item
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string ProductName { get; set; }
    }
}
