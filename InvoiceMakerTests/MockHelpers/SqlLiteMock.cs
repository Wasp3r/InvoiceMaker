using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMakerTests.MockHelpers
{
    public class SqlLiteMock : DataBaseMock
    {
        public override void SetupContainer()
        {
            CleanUp();
            var builder = new ContainerBuilder();
            builder.RegisterType<SqlLiteDataBaseAccess>()
                .As<IDataBaseAccess>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DataAccess>().AsSelf();
            
            _container = builder.Build();
            GetDataBase().Connect($"Data Source={TestPathUtils.TempPath}\\test.db");
        }
    }
}