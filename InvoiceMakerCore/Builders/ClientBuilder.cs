using InvoiceMakerCore.Models;

namespace InvoiceMakerCore.Annotations.Builders
{
    public class ClientBuilder : BaseBuilder<ClientModel>
    {
        public ClientBuilder AddName(string name)
        {
            _result.Name = name;
            return this;
        }

        public ClientBuilder AddCity(string city)
        {
            _result.City = city;
            return this;
        }

        public ClientBuilder AddPostalCode(string postalCode)
        {
            _result.PostalCode = postalCode;
            return this;
        }

        public ClientBuilder AddStreet(string street)
        {
            _result.Street = street;
            return this;
        }
        
        public ClientBuilder AddHomeNumber(string number)
        {
            _result.HomeNumber = number;
            return this;
        }
        
        public ClientBuilder AddApartmentNumber(string number)
        {
            _result.ApartmentNumber = number;
            return this;
        }
        
        public ClientBuilder AddEmail(string email)
        {
            _result.EmailAddress = email;
            return this;
        }
        
        public ClientBuilder AddPhoneNumber(string phoneNumber)
        {
            _result.PhoneNumber = phoneNumber;
            return this;
        }
        
        public ClientBuilder AddNip(string nip)
        {
            _result.NIP = nip;
            return this;
        }
    }
}