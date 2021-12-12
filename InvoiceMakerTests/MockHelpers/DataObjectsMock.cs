using System;
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
        
        public static ClientModel MockClient(int number)
        {
            return new ClientBuilder()
                .AddName($"Test_{number}")
                .Build();
        }

        public static InvoiceModel MockInvoice(ClientModel client, int number)
        {
            return new InvoiceBuilder(client)
                .AddCreationDate(DateTime.Today)
                .AddPaymentTerm(DateTime.Today.AddDays(5))
                .AddPaymentDate(DateTime.Today.AddDays(4))
                .Build();
        }

        public static InvoiceProductEntryModel MockInvoiceEntry(ProductModel product, int number)
        {
            return new InvoiceEntryBuilder(product)
                .AddQuantity(number)
                .AddPricePreUnit(number)
                .Build();
        }
    }
}