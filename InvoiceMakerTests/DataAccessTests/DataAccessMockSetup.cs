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
            var db = SqlLiteMock.SetupDataBase();
            DataAccess = new DataAccess(db);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            SqlLiteMock.CleanUp();
        }
    }
}