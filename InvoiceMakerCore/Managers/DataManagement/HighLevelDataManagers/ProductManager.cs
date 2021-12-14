using System.Collections.Generic;
using System.Data;
using System.Linq;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public class ProductManager : BaseManager, IBaseManager<ProductModel>
    {
        private ProductManager(IDataBaseAccess dataBase)
        {
            _dataBase = dataBase;
        }

        public static ProductManager CreateProductManager(IDataBaseAccess dataBase)
        {
            return new ProductManager(dataBase);
        }

        public void Add(ProductModel newProduct)
        {
            _dataBase.Products.Add(newProduct);
            _dataBase.SaveChanges();
        }

        public ProductModel GetById(int productId)
        {
            return _dataBase.Products.FirstOrDefault(x => x.Id == productId);
        }

        public ProductModel GetByName(string name)
        {
            return _dataBase.Products.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<ProductModel> GetAll()
        {
            return _dataBase.Products;
        }

        public void Remove(int productId)
        {
            var productToBeRemoved = GetById(productId);
            if (productToBeRemoved == null) return;

            if (productToBeRemoved.InvoiceEntries.Any())
            {
                throw new ReadOnlyException($"This product is used in {productToBeRemoved.InvoiceEntries.Count} invoices. " +
                                            $"Please first remove it from existing invoices.");
            }
            
            _dataBase.Products.Remove(productToBeRemoved);
            _dataBase.SaveChanges();
        }
    }
}