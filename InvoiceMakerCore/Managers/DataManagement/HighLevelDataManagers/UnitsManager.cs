using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public class UnitsManager : BaseManager, IBaseManager<UnitModel>
    {
        private UnitsManager(IDataBaseAccess dataBase)
        {
            _dataBase = dataBase;
        }

        public static UnitsManager CreateUnitManager(IDataBaseAccess dataBase)
        {
            return new UnitsManager(dataBase);
        }
        
        public void Add(UnitModel target)
        {
            // TODO: add to error logger
            if (target.Name == "NO_UNIT") return;
            _dataBase.Units.Add(target);
            _dataBase.SaveChanges();
        }

        public UnitModel GetById(int id)
        {
            return _dataBase.Units.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UnitModel> GetByName(string name)
        {
            return _dataBase.Units.Where(x => x.Name == name);
        }

        public IEnumerable<UnitModel> GetAll()
        {
            return _dataBase.Units;
        }

        public void Update(int id, UnitModel newData)
        {
            var unit = GetById(id);
            if (unit == null) return;

            unit.Name = newData.Name;
            _dataBase.SaveChanges();
        }

        public void Remove(int id)
        {
            var unitToBeRemoved = GetById(id);
            if (unitToBeRemoved == null) return;
            _dataBase.Units.Remove(unitToBeRemoved);
            
            var emptyUnit = GetByName("NO_UNIT").FirstOrDefault() ?? GetEmptyUnit();
            foreach (var product in unitToBeRemoved.Products)
            {
                product.Unit = emptyUnit;
            }

            _dataBase.SaveChanges();
        }

        private UnitModel GetEmptyUnit()
        {
            return new UnitBuilder()
                .AddName("NO_UNIT")
                .Build();
        }
    }
}