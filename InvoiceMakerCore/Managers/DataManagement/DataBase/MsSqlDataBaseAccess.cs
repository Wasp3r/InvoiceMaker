using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceMakerCore.Managers.DataManagement.DataBase
{
    public class MsSqlDataBaseAccess : BaseDataBaseAccess
    {
        private string _connectionString { get; set; }
        
        public override void Connect(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionString);
        }
    }
}