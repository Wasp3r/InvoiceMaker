using InvoiceMakerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMakerCore.Managers.DataManagement.DataBase
{
    public interface IDataBaseAccess
    {
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<CurrencyModel> Currencies { get; set; }

        public void Connect(string connectionString);

        public void SaveChanges();
    }
}