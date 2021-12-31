using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceMakerCore.Models
{
    public class ProductModel : DataBaseModel
    {
        public string Name { get; set; }
        public float DefaultPrice { get; set; }
        public int? UnitId { get; set; }

        public UnitModel Unit { get; set; }
        public List<InvoiceProductEntryModel> InvoiceEntries { get; set; } = new();
    }
}