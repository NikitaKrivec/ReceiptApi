using ReceiptApi.Core.Models;

namespace ReceiptApi.Core.Validator
{
    public interface IItemValidator
    {
        bool IsValid(string item);
    }
}