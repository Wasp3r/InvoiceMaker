using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers;

namespace InvoiceMakerCore.Managers.DataManagement
{
    public class DataAccess
    {
        private IDataBaseAccess _dbAccess;
        
        public ClientsManager ClientsManager;
        public ProductManager ProductsManager;

        public DataAccess(IDataBaseAccess dbAccess)
        {
            _dbAccess = dbAccess;
            ClientsManager = ClientsManager.CreateClientManager(dbAccess);
            ProductsManager = ProductManager.CreateProductManager(dbAccess);
        }
    }
}