using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerTests.MockHelpers;
using NUnit.Framework;

namespace InvoiceMakerTests.DataAccessTests
{
    public class DataAccessMockSetup
    {
        protected DataAccess DataAccess;
        private DataBaseMock _dataBaseMock;
        private IDataBaseAccess _dataBase;
        
        [SetUp]
        public void Setup()
        {
            _dataBaseMock = new MsSqlMock();
            _dataBaseMock.SetupContainer();
            DataAccess = _dataBaseMock.GetDataAccess();
            _dataBase = _dataBaseMock.GetDataBase();
        }

        [TearDown]
        public void DropDataBase()
        {
           _dataBase.Disconnect();
           _dataBase.DropDatabase();
        }
    }
}