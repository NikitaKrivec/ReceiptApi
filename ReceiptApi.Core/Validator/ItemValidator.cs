using ReceiptApi.Core.Models;
using ReceiptApi.Service;

namespace ReceiptApi.Core.Validator
{
    public class ItemValidator : IItemValidator
    {
        public bool IsValid(string item)
        {
            return !string.IsNullOrEmpty(item);
        }
    }
}
