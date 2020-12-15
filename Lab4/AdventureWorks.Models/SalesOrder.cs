using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Models
{
    public class SalesOrder
    {
        public int SalesOrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? TerritoryId { get; set; }
        public int? CreditCardId { get; set; }
        public int? ShipToAddressId { get; set; }
        public string AccountNumber { get; set; }
        public string TerritoryName { get; set; }
        public string CardType { get; set; }
        public string City { get; set; }
    }
}