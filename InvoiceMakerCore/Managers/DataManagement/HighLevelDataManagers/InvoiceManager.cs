using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public class InvoiceManager : BaseManager, IBaseManager<InvoiceModel>
    {
        private InvoiceManager(IDataBaseAccess dataBase)
        {
            _dataBase = dataBase;
        }

        public static InvoiceManager CreateClientManager(IDataBaseAccess dataBase)
        {
            return new InvoiceManager(dataBase);
        }
        
        public void Add(InvoiceModel target)
        {
            _dataBase.Invoices.Add(target);
            _dataBase.SaveChanges();
        }

        public InvoiceModel GetById(int id)
        {
            return _dataBase.Invoices.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<InvoiceModel> GetAll()
        {
            return _dataBase.Invoices;
        }

        public void Update(int id, InvoiceModel newData)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public float GetInvoiceSum(InvoiceModel invoice)
        {
            return invoice.Products.Sum(x => x.Quantity * x.PricePerUnit);
        }
    }
}