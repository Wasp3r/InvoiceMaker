using System.Collections.Generic;
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

        public void Update(int productId, ProductModel newProductData)
        {
            var product = GetById(productId);
            if (product == null) return;

            product.Name = newProductData.Name;
            product.DefaultPrice = newProductData.DefaultPrice;
            _dataBase.SaveChanges();
        }

        public void Remove(int productId)
        {
            var productToBeRemoved = GetById(productId);
            if (productToBeRemoved == null) return;
            _dataBase.Products.Remove(productToBeRemoved);
            _dataBase.SaveChanges();
        }
    }
}