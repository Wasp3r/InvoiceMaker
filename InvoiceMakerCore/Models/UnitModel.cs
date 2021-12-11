using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InvoiceMakerCore.Models
{
    public class UnitModel : DataBaseModel
    {
        public string Name { get; set; }
        
        public Collection<ProductModel> Products { get; set; }
    }
}