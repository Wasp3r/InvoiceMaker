using System;
using System.Collections.Generic;

namespace InvoiceMakerCore.Models
{
    public class Invoice : BaseModel
    {
        public int Id { get; }
        public string Number { get; }
        public DateTime CreationDate { get; }
        public DateTime PaymentTerm { get; }
        public DateTime PaymentDate { get; }
        public List<Product> Products { get; set; }
        public Currency Currency { get; set; }

        public Invoice(int id)
        {
            Id = id;
        }
    }
}