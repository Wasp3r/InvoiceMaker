using System;
using System.Collections.Generic;
using InvoiceMakerCore.Managers.DataManagement;
using InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers;
using InvoiceMakerCore.Models;

namespace InvoiceMakerTests.MockHelpers
{
    public static class DataObjectsMock
    {
        public static ProductModel MockProduct(int number)
        {
            return new ProductModel
                {
                    Name = $"Product_{number}",
                    DefaultPrice = number,
                    Unit = MockUnit(0)
                };
        }
        
        public static List<ProductModel> MockProducts(int productsNumber, ProductManager manager)
        {
            var products = new List<ProductModel>();

            for (var i = 0; i < productsNumber; i++)
            {
                var product = MockProduct(i);
                manager.Add(product);
                products.Add(product);
            }

            return products;
        }

        public static UnitModel MockUnit(int number)
        {
            return new UnitModel($"Unit_{number}");
        }
        
        public static ClientModel MockClient(int number)
        {
            return new ClientModel()
            {
                Name = $"Client_{number}"
            };
        }

        public static InvoiceModel MockInvoice(ClientModel client, int number)
        {
            return new InvoiceModel()
            {
                Client = client,
                CreationDate = DateTime.Today,
                PaymentTerm = DateTime.Today.AddDays(5),
                PaymentDate = DateTime.Today.AddDays(4),
                CurrencyModel = MockCurrency(number)
            };
        }

        public static InvoiceProductEntryModel MockInvoiceEntry(ProductModel product, int number)
        {
            return new InvoiceProductEntryModel()
            {
                Product = product,
                Quantity = number,
                PricePerUnit = number
            };
        }

        public static CurrencyModel MockCurrency(int i)
        {
            return new CurrencyModel($"Currency_{i}");
        }
    }
}