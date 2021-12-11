using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public class ClientsManager : BaseManager, IBaseManager<ClientModel>
    {
        private ClientsManager(IDataBaseAccess dataBase)
        {
            _dataBase = dataBase;
        }

        public static ClientsManager CreateClientManager(IDataBaseAccess dataBase)
        {
            return new ClientsManager(dataBase);
        }

        public void Add(ClientModel client)
        {
            _dataBase.Clients.Add(client);
            _dataBase.SaveChanges();
        }

        public ClientModel GetById(int id)
        {
            return _dataBase.Clients.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ClientModel> GetByName(string name)
        {
            return _dataBase.Clients.Where(x => x.Name == name);
        }

        public IEnumerable<ClientModel> GetAll()
        {
            return _dataBase.Clients;
        }

        public void Update(int clientId, ClientModel newClientData)
        {
            var client = GetById(clientId);
            if (client == null) return;

            client.Name = newClientData.Name;
            _dataBase.SaveChanges();
        }

        public void Remove(int clientId)
        {
            var clientToBeRemoved = GetById(clientId);
            if (clientToBeRemoved == null) return;
            _dataBase.Clients.Remove(clientToBeRemoved);
            _dataBase.SaveChanges();
        }
    }
}