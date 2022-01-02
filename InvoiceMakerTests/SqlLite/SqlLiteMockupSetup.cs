using System.IO;
using System.Threading.Tasks;
using InvoiceMakerCore.Managers;
using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;
using InvoiceMakerTests.MockHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;

namespace InvoiceMakerTests.SqlLite
{
    public class SqlLiteMockupSetup
    {
        internal IDataBaseAccess DataBaseAccess;
        private DataBaseMock _dataBaseMock;

        [OneTimeSetUp]
        public void Setup()
        {
            _dataBaseMock = new SqlLiteMock();
            _dataBaseMock.SetupContainer();
            DataBaseAccess = _dataBaseMock.GetDataBase();
        }

        [TearDown]
        public void DropDataBase()
        {
            DataBaseAccess.Disconnect();
            DataBaseAccess.ClearDatabase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            DataBaseAccess.DropDatabase();
        }
    }
}