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

        public override void Connect(string connectionString)
        {
            _dbPath = connectionString;
            if (File.Exists(_dbPath)) return;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>()
                .HasOne(prod => prod.Unit)
                .WithMany(x => x.Products)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}