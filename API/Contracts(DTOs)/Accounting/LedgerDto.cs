namespace App_Sales.DTO.AccountingDTO
{
    public class LedgerDto
    {
        public string AccountName { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
    }
}
