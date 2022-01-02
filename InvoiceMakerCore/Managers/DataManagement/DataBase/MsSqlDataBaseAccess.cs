using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceMakerCore.Managers.DataManagement.DataBase
{
    public class MsSqlDataBaseAccess : BaseDataBaseAccess
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionString);
        }
    }
}