using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using InvoiceMakerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMakerCore.Managers.DataManagement.DataBase
{
    public class SqlLiteDataBaseAccess : BaseDataBaseAccess
    {
        private string _dbPath { get; set; }

        private Dictionary<Type, string> _sqlTypeMap => new ()
        {
            {typeof(string), "TEXT"},
            {typeof(int), "INTEGER"},
            {typeof(float), "REAL"},
            {typeof(double), "REAL"}
        };

        public override void Connect(string connectionString)
        {
            _dbPath = connectionString;
            if (File.Exists(_dbPath)) return;
            CreateTables();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_dbPath}");
        }

        private void CreateTables()
        {
            Database.ExecuteSqlRaw(GetTableCreationCommand<ClientModel>(nameof(Clients)));
            Database.ExecuteSqlRaw(GetTableCreationCommand<ProductModel>(nameof(Products)));
            Database.ExecuteSqlRaw(GetTableCreationCommand<CurrencyModel>(nameof(Currencies)));
            Database.ExecuteSqlRaw(GetTableCreationCommand<InvoiceModel>(nameof(Invoices)));
            Database.ExecuteSqlRaw(GetTableCreationCommand<InvoiceProductEntryModel>(nameof(InvoiceProductEntries)));
        }

        private string GetTableCreationCommand<T>(string propertyName) where T : DataBaseModel
        {
            var stringBuilder = new StringBuilder();
            var type = typeof(T);
            var attributes = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            stringBuilder.Append($"CREATE TABLE IF NOT EXISTS '{propertyName}' (");
            stringBuilder.Append("'Id' INTEGER PRIMARY KEY AUTOINCREMENT");
            foreach (var attribute in attributes)
            {
                if (attribute.Name == "Id") continue;
                stringBuilder.Append($", '{attribute.Name}' ");
                var sqlType = _sqlTypeMap.FirstOrDefault(x => x.Key == attribute.PropertyType).Value;
                stringBuilder.Append(string.IsNullOrEmpty(sqlType) ? "BLOB" : sqlType);
            }
            stringBuilder.Append(")");
            
            return stringBuilder.ToString();
        }
    }
}