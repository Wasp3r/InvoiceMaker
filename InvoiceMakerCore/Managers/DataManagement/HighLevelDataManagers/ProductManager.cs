using System.Collections.Generic;
using System.Linq;
using InvoiceMakerCore.Managers.DataManagement.DataBase;
using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Managers.DataManagement.HighLevelDataManagers
{
    public class ProductManager : BaseManager
    {
        private ProductManager(IDataBaseAccess dataBase)
        {
            _dataBase = dataBase;
        }

        public static ProductManager CreateProductManager(IDataBaseAccess dataBase)
        {
            return new ProductManager(dataBase);
        }

        public void AddProduct(ProductModel newProduct)
        {
            _dataBase.Products.Add(newProduct);
            _dataBase.SaveChanges();
        }

        public ProductModel GetProductByName(string name)
        {
            return _dataBase.Products.FirstOrDefault(x => x.Name == name);
        }

        public ProductModel GetProductById(int productId)
        {
            return _dataBase.Products.FirstOrDefault(x => x.Id == productId);
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return _dataBase.Products;
        }

        public void UpdateProduct(int productId, ProductModel newProductData)
        {
            var product = GetProductById(productId);
            if (product == null) return;

            product.Name = newProductData.Name;
            product.DefaultPrice = newProductData.DefaultPrice;
            _dataBase.SaveChanges();
        }

        public void RemoveProduct(int productId)
        {
            var productToBeRemoved = GetProductById(productId);
            if (productToBeRemoved == null) return;
            _dataBase.Products.Remove(productToBeRemoved);
            _dataBase.SaveChanges();
        }
    }
}