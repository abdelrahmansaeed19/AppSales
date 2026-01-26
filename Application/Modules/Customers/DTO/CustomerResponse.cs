using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.DTO
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal CurrentBalance { get; set; }
    }

    public class CustomerStatementResponse
    {
        public CustomerResponse Customer { get; set; } = new();
        public List<TransactionDto> Transactions { get; set; } = new();
    }

    public class TransactionDto
    {
        public string Type { get; set; } = string.Empty; // invoice, payment
        public string Description { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }
    }
}
