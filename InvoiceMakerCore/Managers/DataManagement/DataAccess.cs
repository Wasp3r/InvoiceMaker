using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers;

namespace InvoiceMakerCore.Managers.DataManagement
{
    public class DataAccess
    {
        private IDataBaseAccess _dbAccess;
        
        public ClientsManager ClientsManager;

        public DataAccess(IDataBaseAccess dbAccess)
        {
            _dbAccess = dbAccess;
            ClientsManager = ClientsManager.CreateClientManager(dbAccess);
        }
    }
}