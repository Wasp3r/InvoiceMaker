using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public class CurrencyManager : BaseManager, IBaseManager<CurrencyModel>
    {
        private CurrencyManager(IDataBaseAccess dataBase)
        {
            _dataBase = dataBase;
        }

        public static CurrencyManager CreateCurrencyManager(IDataBaseAccess dataBase)
        {
            return new CurrencyManager(dataBase);
        }
        
        public void Add(CurrencyModel target)
        {
            _dataBase.Currencies.Add(target);
            _dataBase.SaveChanges();
        }

        public CurrencyModel GetById(int id)
        {
            return _dataBase.Currencies.FirstOrDefault(x => x.Id == id);
        }
        
        public IEnumerable<CurrencyModel> GetByName(string name)
        {
            return _dataBase.Currencies.Where(x => x.Name == name);
        }

        public IEnumerable<CurrencyModel> GetAll()
        {
            return _dataBase.Currencies;
        }

        public void Remove(int id)
        {
            var currencyToBeRemoved = GetById(id);
            if (currencyToBeRemoved == null) return;
            
            _dataBase.Currencies.Remove(currencyToBeRemoved);
            var emptyCurrency = GetByName("NO_UNIT").FirstOrDefault() ?? GetEmptyCurrency();
            foreach (var invoice in currencyToBeRemoved.Invoices)
            {
                invoice.CurrencyModel = emptyCurrency;
            }
            
            _dataBase.SaveChanges();
        }
        
        private CurrencyModel GetEmptyCurrency()
        {
            return new CurrencyBuilder()
                .AddName("NO_CURRENCY")
                .Build();
        }
    }
}