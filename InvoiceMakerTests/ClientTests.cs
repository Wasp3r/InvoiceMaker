using InvoiceMakerCore.Models;
using NUnit.Framework;

namespace InvoiceMakerTests
{
    public class ClientTests
    {
        [Test]
        public void ClientPropertiesTest()
        {
            var user = new Client(0);
            var propertyChangedTriggered = false;
            user.PropertyChanged += (sender, args) =>
            {
                propertyChangedTriggered = true;
            };

            user.Name = "John";
            Assert.AreEqual("John", user.Name);
            Assert.IsTrue(propertyChangedTriggered);
        }
    }
}