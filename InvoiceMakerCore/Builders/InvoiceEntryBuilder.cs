using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Annotations.Builders
{
    public class InvoiceEntryBuilder : BaseBuilder<InvoiceProductEntryModel>
    {
        public InvoiceEntryBuilder(ProductModel product)
        {
            _result = new InvoiceProductEntryModel
            {
                Product = product
            };
        }
        
        public InvoiceEntryBuilder AddPricePreUnit(float price)
        {
            _result.PricePerUnit = price;
            return this;
        }

        public InvoiceEntryBuilder AddQuantity(float quantity)
        {
            _result.Quantity = quantity;
            return this;
        }
    }
}