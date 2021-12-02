namespace InvoiceMakerCore.Models
{
    public class Currency
    {
        public int Id { get; }
        public string Name { get; set; }

        public Currency(int id)
        {
            Id = id;
        }
    }
}