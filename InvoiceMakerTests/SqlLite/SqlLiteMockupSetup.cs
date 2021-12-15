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
        
        [SetUp]
        public void Setup()
        {
            SqlLiteMock.SetupContainer();
            DataBaseAccess = SqlLiteMock.GetDataBase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            SqlLiteMock.CleanUp();
        }
    }
}