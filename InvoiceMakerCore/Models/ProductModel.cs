using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using InvoiceMakerCore.Annotations.Builders;

namespace InvoiceMakerCore.Models
{
    public class ProductModel : DataBaseModel
    {
        public string Name { get; set; }
        public float DefaultPrice { get; set; }
        public int? UnitId { get; set; }

        public UnitModel Unit { get; set; }
    }
}