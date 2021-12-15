using System.IO;
using Autofac;
using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.DataBase;

namespace InvoiceMakerTests.MockHelpers
{
    public static class SqlLiteMock
    {
        public static IContainer Container;
        
        public static void SetupContainer()
        {
            CleanUp();
            var builder = new ContainerBuilder();
            builder.RegisterType<SqlLiteDataBaseAccess>()
                .As<IDataBaseAccess>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<DataAccess>().AsSelf();
            Container = builder.Build();
            Container.Resolve<IDataBaseAccess>().Connect($"{TestPathUtils.TempPath}\\test.db");
        }

        public static DataAccess GetDataAccess()
        {
            using var scope = Container.BeginLifetimeScope();
            return Container.Resolve<DataAccess>();
        }

        public static IDataBaseAccess GetDataBase()
        {
            using var scope = Container.BeginLifetimeScope();
            return Container.Resolve<IDataBaseAccess>();
        }

        public static void CleanUp()
        {
            if (!Directory.Exists(TestPathUtils.TempPath)) return;
            Directory.Delete(TestPathUtils.TempPath, true);
        }
    }
}