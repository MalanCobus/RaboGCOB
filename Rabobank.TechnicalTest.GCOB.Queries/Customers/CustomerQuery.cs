using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Queries.Customers
{
    public class CustomerQuery
    {
        /// <summary>
        /// Retrieve the customer
        /// </summary>
        /// <param name="customerId">The Id of the customer.</param>
        public CustomerQuery(int customerId)
        {
            this.CustomerId = customerId;
        }

        public int CustomerId { get; }
        
    }
}
