using Microsoft.EntityFrameworkCore;
using ReceiptApi.Core.Models;
using ReceiptApi.Data;

namespace ReceiptApi.Service
{
    public class ReceiptService : IReceiptService
    {
        private readonly DataContext _context;

        public ReceiptService(DataContext context)
        {
            _context = context;
        }

        public List<Receipt> GetReceipts()
        {
            return _context.Receipts.Include(f => f.ReceiptItems).ToList();
        }

        public Receipt AddReceipt(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            _context.SaveChanges();
            return receipt;
        }

        public string DeleteAllReceipts()
        {
            _context.Receipts.RemoveRange(_context.Receipts);
            _context.Items.RemoveRange(_context.Items);
            _context.SaveChanges();

            return "Receipt list was successfully deleted";
        }

        public Receipt GetReceiptById(int id)
        {
            return _context.Receipts.Include(f => f.ReceiptItems).SingleOrDefault(x => x.Id == id);
        }

        public Receipt DeleteReceiptById(int id)
        {
            var receipt = GetReceiptById(id);
            _context.Receipts.Remove(receipt);
            _context.SaveChanges();

            return receipt;
        }
        public List<Receipt> GetReceiptsByCreationDate(DateTime startT, DateTime endT)
        {
            return _context.Receipts.Where(t => t.CreatedOn >= startT && t.CreatedOn <= endT).ToList();
        }

        public List<Receipt> GetReceiptsByItemProductName(string name)
        {
            return _context.Receipts.Include(f => f.ReceiptItems).
                Where(n => n.ReceiptItems.Any(p => p.ProductName == name)).ToList();
        }

        public bool ReceiptIdExists(int id)
        {
            return _context.Receipts.Any(x => x.Id == id);
        }

        public bool IsTimeValid(DateTime time)
        {
            return !string.IsNullOrEmpty(time.ToString());
        }

        public bool ProductNameExists(string name)
        {
            return _context.Items.Any(x => x.ProductName == name);
        }
    }
}
