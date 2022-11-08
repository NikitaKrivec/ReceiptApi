using ReceiptApi.Core.Models;

namespace ReceiptApi.Core.Validator
{
    public interface IReceiptValidator
    {
        bool IsInvalidId(int id);
        bool IsReceiptExist(Receipt receipt);
    }
}