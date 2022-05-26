using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Rabobank.TechnicalTest.GCOB.Api.Models.Customers
{
    public class AddCustomerDto
    {
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        [Required] 
        public string Surname { get; set; }

        [JsonPropertyName("street")]
        [Required] 
        public string Street { get; set; }

        [JsonPropertyName("city")]
        [Required] 
        public string City { get; set; }

        [JsonPropertyName("postcode")]
        [Required] 
        public string Postcode { get; set; }

        [JsonPropertyName("country")]
        [Required]
        public string Country { get; set; }
    }
}
