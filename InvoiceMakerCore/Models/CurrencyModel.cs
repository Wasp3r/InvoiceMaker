using System.Collections.Generic;

namespace InvoiceMakerCore.Models
{
    public class CurrencyModel : DataBaseModel
    {
        public string Name { get; set; }

        public List<InvoiceModel> Invoices { get; set; } = new();
    }
}