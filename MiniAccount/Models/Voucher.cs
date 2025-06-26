using Microsoft.AspNetCore.Mvc;

namespace MiniAccount.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string? VoucherType { get; set; } // Journal, Payment, Receipt
        public string? ReferenceNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public List<VoucherEntry> Entries { get; set; } = new();
    }
}
