namespace MiniAccount.Models.ViewModel
{
    public class VoucherDetailsViewModel
    {
        public int VoucherId { get; set; }
        public string VoucherType { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? VoucherDate { get; set; }

        public int EntryId { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
    }
}
