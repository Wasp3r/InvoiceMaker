using System;
using System.Collections.Generic;

namespace InvoiceMakerCore.Models
{
    public class InvoiceModel : DataBaseModel
    {
        public string Number { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PaymentTerm { get; set; }
        public DateTime PaymentDate { get; set; }
        public List<InvoiceProductEntryModel> Products { get; set; }
        public int CurrencyModelId { get; set; }
        public CurrencyModel CurrencyModel { get; set; }
    }
}