using Autofac;
using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.DataBase;

namespace InvoiceMakerTests.MockHelpers
{
    public abstract class DataBaseMock
    {
        protected IContainer _container;

        public virtual DataAccess GetDataAccess()
        {
            return _container.Resolve<DataAccess>();
        }

        public virtual IDataBaseAccess GetDataBase()
        {
            return _container.Resolve<IDataBaseAccess>();
        }

        public abstract void SetupContainer();
        public abstract void CleanUp();
    }
}