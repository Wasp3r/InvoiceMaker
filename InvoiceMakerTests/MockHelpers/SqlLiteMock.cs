using System.IO;
using InvoiceMakerCore.Managers.DataManagement.DataBase;

namespace InvoiceMakerTests.MockHelpers
{
    public static class SqlLiteMock
    {
        public static SqlLiteDataBaseAccess SetupDataBase()
        {
            CleanUp();
            var dataBaseAccess = new SqlLiteDataBaseAccess();
            dataBaseAccess.Connect($"{TestPathUtils.TempPath}\\test.db");
            return dataBaseAccess;
        }

        public static void CleanUp()
        {
            if (!Directory.Exists(TestPathUtils.TempPath)) return;
            Directory.Delete(TestPathUtils.TempPath, true);
        }
    }
}