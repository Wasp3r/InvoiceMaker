using System.ComponentModel;
using System.Runtime.CompilerServices;
using InvoiceMakerCore.Annotations;

namespace InvoiceMakerCore.Models
{
    public class ClientModel : DataBaseModel
    {
        public string Name { get; set; }
    }
}