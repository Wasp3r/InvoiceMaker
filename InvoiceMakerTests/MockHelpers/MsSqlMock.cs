using System.IO;
using Autofac;
using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.DataBase;

namespace InvoiceMakerTests.MockHelpers
{
    public class MsSqlMock : DataBaseMock
    {
        public override void SetupContainer()
        {
            CleanUp();
            var builder = new ContainerBuilder();
            builder.RegisterType<MsSqlDataBaseAccess>()
                .As<IDataBaseAccess>()
                .SingleInstance();
            builder.RegisterType<DataAccess>().AsSelf();
            
            _container = builder.Build();
            _container.Resolve<IDataBaseAccess>().Connect($"Server=(localdb)\\mssqllocaldb;Database={TestPathUtils.TempPath}\\test; Trusted_Connection=True;");
        }
    }
}