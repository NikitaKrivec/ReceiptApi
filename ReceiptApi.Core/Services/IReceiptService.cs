using ReceiptApi.Core.Models;

namespace ReceiptApi.Service
{
    public interface IReceiptService
    {
        Receipt AddReceipt(Receipt receipt);
        string DeleteAllReceipts();
        Receipt DeleteReceiptById(int id);
        Receipt GetReceiptById(int id);
        List<Receipt> GetReceipts();
        List<Receipt> GetReceiptsByCreationDate(DateTime from, DateTime to);
        List<Receipt> GetReceiptsByItemProductName(string name);
        bool ProductNameExists(string name);
        bool ReceiptIdExists(int id);
        bool IsTimeValid(DateTime time);
    }
}