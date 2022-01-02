using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerTests.MockHelpers;
using NUnit.Framework;

namespace InvoiceMakerTests.DataAccessTests
{
    public class DataAccessMockSetup
    {
        protected DataAccess DataAccess;
        
        [SetUp]
        public void Setup()
        {
            SqlLiteMock.SetupContainer();
            DataAccess = SqlLiteMock.GetDataAccess();
        }

        [TearDown]
        public void DropDataBase()
        {
            SqlLiteMock.GetDataBase().DropDatabase();
        }
    }
}