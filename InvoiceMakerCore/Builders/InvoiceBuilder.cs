using System;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Annotations.Builders
{
    public class InvoiceBuilder : BaseBuilder<InvoiceModel>
    {
        public InvoiceBuilder(ClientModel client)
        {
            _result.Client = client;
        }
        
        public InvoiceBuilder AddCreationDate(DateTime date)
        {
            _result.CreationDate = date;
            return this;
        }

        public InvoiceBuilder AddPaymentTerm(DateTime date)
        {
            _result.PaymentTerm = date;
            return this;
        }

        public InvoiceBuilder AddPaymentDate(DateTime date)
        {
            _result.PaymentDate = date;
            return this;
        }

        public InvoiceBuilder AddProductEntry(InvoiceProductEntryModel entry)
        {
            _result.Products.Add(entry);
            return this;
        }

        public InvoiceBuilder AddCurrency(CurrencyModel currency)
        {
            _result.CurrencyModel = currency;
            return this;
        }
    }
}