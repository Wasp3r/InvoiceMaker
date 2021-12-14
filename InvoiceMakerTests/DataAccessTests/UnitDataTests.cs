using System.Linq;
using InvoiceMakerCore.Annotations.Builders;
using InvoiceMakerCore.Models;
using InvoiceMakerTests.MockHelpers;
using NUnit.Framework;

namespace InvoiceMakerTests.DataAccessTests
{
    public class UnitDataTests : DataAccessMockSetup
    {
        [TestCase(1)]
        [TestCase(5)]
        public void CreateUnitsTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataAccess.UnitsManager.Add(DataObjectsMock.MockUnit(i));
                Assert.AreEqual(1, DataAccess.UnitsManager.GetByName($"Unit_{i}").Count());
            }
            
            Assert.AreEqual(number, DataAccess.UnitsManager.GetAll().Count());
        }

        [Test]
        public void InvalidUnitCreationTest()
        {
            var invalidUnit = new UnitBuilder()
                .AddName("NO_UNIT")
                .Build();
            DataAccess.UnitsManager.Add(invalidUnit);
            DataAccess.UnitsManager.Add(DataObjectsMock.MockUnit(1));
            
            Assert.IsNotEmpty(DataAccess.UnitsManager.GetAll());
            Assert.IsEmpty(DataAccess.UnitsManager.GetByName("NO_UNIT"));
        }
        
        [TestCase(1)]
        [TestCase(5)]
        public void GetUnitByIdTest(int number)
        {
            for (var i = 0; i < number; i++)
            {
                DataAccess.UnitsManager.Add(DataObjectsMock.MockUnit(i));
            }

            for (var i = 1; i < number +1; i++)
            {
                var unit = DataAccess.UnitsManager.GetById(i);
                Assert.NotNull(unit);
                Assert.AreEqual($"Unit_{i - 1}", unit.Name);
            }
        }
        
        [Test]
        public void UpdateUnitTest()
        {
            var unit = DataObjectsMock.MockUnit(0);
            DataAccess.UnitsManager.Add(unit);
            
            Assert.AreEqual("Unit_0", unit.Name);
            unit.Name = "Updated Unit";
            DataAccess.SaveChanges();
            
            Assert.AreEqual("Updated Unit", DataAccess.UnitsManager.GetById(1).Name);
        }
        
        [Test]
        public void RemoveUnitTest()
        {
            var unit_0 = DataObjectsMock.MockUnit(0);
            var unit_1 = DataObjectsMock.MockUnit(1);
            DataAccess.UnitsManager.Add(unit_0);
            DataAccess.UnitsManager.Add(unit_1);
            
            var product = DataObjectsMock.MockProduct(0);
            product.Unit = unit_1;
            DataAccess.ProductsManager.Add(product);

            Assert.AreEqual(2, DataAccess.UnitsManager.GetAll().Count());
            DataAccess.UnitsManager.Remove(2);
            Assert.IsEmpty(DataAccess.UnitsManager.GetByName("Unit_1"));

            var loadedProduct = DataAccess.ProductsManager.GetById(1);
            Assert.AreEqual("NO_UNIT", loadedProduct.Unit.Name);
            
        }
    }
}