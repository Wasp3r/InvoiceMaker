namespace InvoiceMakerCore.Models
{
    public class InvoiceProductEntryModel : DataBaseModel
    {
        public int InvoiceId { get; set; }
        public InvoiceModel Invoice { get; set; }
        public int? ProductId { get; set; }
        public ProductModel Product { get; set; }
        public float Quantity { get; set; }
        public float PricePerUnit { get; set; }
    }
}