using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public class ClientsManager : BaseManager
    {
        private ClientsManager(IDataBaseAccess dataBase)
        {
            _dataBase = dataBase;
        }

        public static ClientsManager CreateClientManager(IDataBaseAccess dataBase)
        {
            return new ClientsManager(dataBase);
        }

        public void CreateClient(string name)
        {
            _dataBase.Clients.Add(new ClientModel() { Name = name });
            _dataBase.SaveChanges();
        }
        
        public ClientModel GetClientById(int id)
        {
            return _dataBase.Clients.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ClientModel> GetClientsByName(string name)
        {
            return _dataBase.Clients.Where(x => x.Name == name);
        }

        public IEnumerable<ClientModel> GetAllClients()
        {
            return _dataBase.Clients;
        }

        public void UpdateClient(int clientId, ClientModel newClientData)
        {
            var client = GetClientById(clientId);
            if (client == null) return;

            client.Name = newClientData.Name;
            _dataBase.SaveChanges();
        }

        public void RemoveClient(int clientId)
        {
            var clientToBeRemoved = GetClientById(clientId);
            if (clientToBeRemoved == null) return;
            _dataBase.Clients.Remove(clientToBeRemoved);
            _dataBase.SaveChanges();
        }
    }
}