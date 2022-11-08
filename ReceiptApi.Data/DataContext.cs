using Microsoft.EntityFrameworkCore;
using ReceiptApi.Core.Models;

namespace ReceiptApi.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Item> Items { get; set; }
    }
}
