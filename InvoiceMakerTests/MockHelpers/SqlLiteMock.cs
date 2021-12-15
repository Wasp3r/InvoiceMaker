using System.IO;
using Autofac;
using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.DataBase;

namespace InvoiceMakerTests.MockHelpers
{
    public static class SqlLiteMock
    {
        private static IContainer _container;
        
        public static void SetupContainer()
        {
            CleanUp();
            var builder = new ContainerBuilder();
            builder.RegisterType<SqlLiteDataBaseAccess>()
                .As<IDataBaseAccess>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<DataAccess>().AsSelf();
            _container = builder.Build();
            _container.Resolve<IDataBaseAccess>().Connect($"{TestPathUtils.TempPath}\\test.db");
        }

        public static DataAccess GetDataAccess()
        {
            return _container.Resolve<DataAccess>();
        }

        public static IDataBaseAccess GetDataBase()
        {
            return _container.Resolve<IDataBaseAccess>();
        }

        public static void CleanUp()
        {
            if (!Directory.Exists(TestPathUtils.TempPath)) return;
            Directory.Delete(TestPathUtils.TempPath, true);
        }
    }
}