using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMakerCore.Managers.DataManagement
{
    public abstract class BaseDataBaseAccess : DbContext, IDataBaseAccess
    {
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<InvoiceProductEntryModel> InvoiceProductEntries { get; set; }
        public DbSet<CurrencyModel> Currencies { get; set; }
        public DbSet<UnitModel> Units { get; set; }
        
        public virtual void Connect(string connectionString) { }

        public virtual void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}