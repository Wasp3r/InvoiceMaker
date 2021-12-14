using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public class InvoiceManager : BaseManager, IBaseManager<InvoiceModel>
    {
        private InvoiceManager(IDataBaseAccess dataBase)
        {
            _dataBase = dataBase;
        }

        public static InvoiceManager CreateClientManager(IDataBaseAccess dataBase)
        {
            return new InvoiceManager(dataBase);
        }
        
        public void Add(InvoiceModel target)
        {
            var invoices = GetAll().Where(x => x.CreationDate.Year == DateTime.Today.Year);
            target.Number = $"{DateTime.Today.Year}/{invoices.Count()+1}";
            _dataBase.Invoices.Add(target);
            _dataBase.SaveChanges();
        }

        public InvoiceModel GetById(int id)
        {
            return _dataBase.Invoices.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<InvoiceModel> GetAll()
        {
            return _dataBase.Invoices;
        }

        public IEnumerable<InvoiceModel> GetAllAfterDeadline()
        {
            return _dataBase.Invoices.Where(x =>
                x.PaymentDate == new DateTime() &&
                x.PaymentTerm < DateTime.Today);
        }

        public IEnumerable<InvoiceModel> GetAllWithTerm(int days)
        {
            return _dataBase.Invoices.Where(x =>
                x.PaymentDate == new DateTime() &&
                x.PaymentTerm < DateTime.Today.AddDays(days));
        }

        public void Remove(int id)
        {
            var invoiceToBeRemoved = GetById(id);
            if (invoiceToBeRemoved == null) return;

            _dataBase.Invoices.Remove(invoiceToBeRemoved);
            _dataBase.SaveChanges();
        }

        public float GetInvoiceSum(InvoiceModel invoice)
        {
            return invoice.Products.Sum(x => x.Quantity * x.PricePerUnit);
        }

        public bool IsAfterDeadline(InvoiceModel invoice)
        {
            if (invoice.PaymentDate != new DateTime()) return false;
            return invoice.PaymentTerm < DateTime.Today;
        }
    }
}