using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Models;

namespace InvoiceMakerTests.MockHelpers
{
    public static class DataObjectsMock
    {
        public static ProductModel MockProduct(int number)
        {
            return new ProductBuilder()
                .AddName($"Product_{number}")
                .AddDefaultPrice(number)
                .AddUnit(MockUnit(0))
                .Build();
        }

        public static UnitModel MockUnit(int number)
        {
            return new UnitBuilder()
                .AddName($"Unit_{number}")
                .Build();
        }
    }
}