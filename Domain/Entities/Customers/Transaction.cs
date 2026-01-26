using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Customers
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? Type { get; set; } // invoice, payment
        public string? Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; } 
        public Customer Customer { get; set; } 
    }
}
