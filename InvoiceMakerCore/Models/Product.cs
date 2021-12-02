using System.ComponentModel;

namespace InvoiceMakerCore.Models
{
    public class Product : BaseModel
    {
        public int Id { get; }
        public string Name { get; set; }
        public float DefaultPrice { get; set; }

        public Product(int id)
        {
            Id = id;
        }
    }
}