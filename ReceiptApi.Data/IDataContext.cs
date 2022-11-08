using Microsoft.EntityFrameworkCore;
using ReceiptApi.Core.Models;

namespace ReceiptApi.Data
{
    public interface IDataContext
    {
        DbSet<Item> Items { get; set; }
        DbSet<Receipt> Receipts { get; set; }
    }
}