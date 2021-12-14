using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Annotations.Builders
{
    public class CurrencyBuilder : BaseBuilder<CurrencyModel>
    {
        public CurrencyBuilder AddName(string name)
        {
            _result.Name = name;
            return this;
        }
    }
}