using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Annotations.Builders
{
    public class ProductBuilder : BaseBuilder<ProductModel>
    {
        public ProductBuilder AddName(string productName)
        {
            _result.Name = productName;
            return this;
        }

        public ProductBuilder AddDefaultPrice(float price)
        {
            _result.DefaultPrice = price;
            return this;
        }
        
        public ProductBuilder AddUnit(UnitModel unit)
        {
            _result.Unit = unit;
            return this;
        }
    }
}