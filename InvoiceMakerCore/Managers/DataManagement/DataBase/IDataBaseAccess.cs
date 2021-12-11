using InvoiceMakerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMakerCore.Managers.DataManagement.DataBase
{
    public interface IDataBaseAccess
    {
        public DbSet<ClientModel> Clients { get; }
        public DbSet<ProductModel> Products { get; }
        public DbSet<InvoiceModel> Invoices { get; }
        public DbSet<CurrencyModel> Currencies { get; }
        public DbSet<UnitModel> Units { get; }

        public void Connect(string connectionString);

        public void SaveChanges();
    }
}