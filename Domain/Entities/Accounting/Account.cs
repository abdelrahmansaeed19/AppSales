namespace Domain.Entities.Accounts
{
    public class Account
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
    }

    public enum AccountType
    {
        Asset,
        Liability,
        Equity,
        Revenue,
        Expense
    }
}

