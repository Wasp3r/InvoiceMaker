using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Annotations.Builders
{
    public class UnitBuilder : BaseBuilder<UnitModel>
    {
        public UnitBuilder AddName(string name)
        {
            _result.Name = name;
            return this;
        }
    }
}