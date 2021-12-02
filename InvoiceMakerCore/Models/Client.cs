using System.ComponentModel;
using System.Runtime.CompilerServices;
using InvoiceMakerCore.Annotations;

namespace InvoiceMakerCore.Models
{
    public class Client : BaseModel
    {
        public int Id { get; }
        
        public string Name { get; set; }

        public Client(int id)
        {
            Id = id;
        }
    }
}