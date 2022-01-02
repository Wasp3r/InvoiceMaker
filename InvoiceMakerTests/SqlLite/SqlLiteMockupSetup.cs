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
        
        [OneTimeSetUp]
        public void Setup()
        {
            SqlLiteMock.SetupContainer();
            DataBaseAccess = SqlLiteMock.GetDataBase();
        }

        [TearDown]
        public void DropDataBase()
        {
            SqlLiteMock.GetDataBase().ClearDatabase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            SqlLiteMock.GetDataBase().DropDatabase();
        }
    }
}