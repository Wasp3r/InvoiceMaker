using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InvoiceMakerCore.Annotations;

namespace InvoiceMakerCore.Models
{
    public class ClientModel : DataBaseModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public string  ApartmentNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string NIP { get; set; }
        public List<InvoiceModel> Invoices { get; set; } = new();
    }
}