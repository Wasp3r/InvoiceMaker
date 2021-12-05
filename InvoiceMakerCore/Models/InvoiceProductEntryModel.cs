namespace InvoiceMakerCore.Models
{
    public class InvoiceProductEntryModel : DataBaseModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public float Quantity { get; set; }
        public float PricePerUnit { get; set; }
    }
}