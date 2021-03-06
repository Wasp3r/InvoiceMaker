using System.Collections.Generic;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;
using InvoiceMakerCore.Models.Base;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public interface IBaseManager<T> where T : BaseModel
    {
        public void Add(T target);
        
        public T GetById(int id);

        public IEnumerable<T> GetAll();

        public void Remove(int id);
    }
}