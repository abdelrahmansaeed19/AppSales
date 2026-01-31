using System.Transactions;
using System.Collections.Generic;
namespace Domain.Entities.Customers
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
         public string? Email { get; set; }
        public string? Address { get; set; }

        public string? Phone { get; set; }
    }

}