namespace Rabobank.TechnicalTest.GCOB.Commands.Customers
{
    public class AddCustomerCommand
    {
        /// <summary>
        /// Create the customer command
        /// </summary>
        /// <param name="firstName">The name of the customer.</param>
        /// <param name="lastName">The surname of the customer.</param>
        /// <param name="street">The street of the customer.</param>
        /// <param name="city">The city of the customer.</param>
        /// <param name="postalcode">The postalcode of the customer.</param>
        /// <param name="country">The country of the customer.</param>
        public AddCustomerCommand(string firstName, string lastName, string street, string city, string postalcode, string country)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Street = street;
            this.City = city;
            this.PostalCode = postalcode;
            this.Country = country;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

    }
}
