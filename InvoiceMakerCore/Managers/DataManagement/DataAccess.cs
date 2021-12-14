using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers;

namespace InvoiceMakerCore.Managers.DataManagement
{
    public class DataAccess
    {
        private IDataBaseAccess _dbAccess;
        
        public ClientsManager ClientsManager;
        public ProductManager ProductsManager;
        public UnitsManager UnitsManager;
        public InvoiceManager InvoiceManager;
        public CurrencyManager CurrencyManager;

        public DataAccess(IDataBaseAccess dbAccess)
        {
            _dbAccess = dbAccess;
            ClientsManager = ClientsManager.CreateClientManager(dbAccess);
            ProductsManager = ProductManager.CreateProductManager(dbAccess);
            UnitsManager = UnitsManager.CreateUnitManager(dbAccess);
            InvoiceManager = InvoiceManager.CreateClientManager(dbAccess);
            CurrencyManager = CurrencyManager.CreateCurrencyManager(dbAccess);
        }

        public void SaveChanges()
        {
            _dbAccess.SaveChanges();
        }
    }
}