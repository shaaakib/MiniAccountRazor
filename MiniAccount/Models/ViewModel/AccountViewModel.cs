namespace MiniAccount.Models.ViewModel
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public string AccountType { get; set; }
        public int? ParentAccountId { get; set; }
        public string? ParentAccountName { get; set; }
        public bool IsActive { get; set; }
    }
}
