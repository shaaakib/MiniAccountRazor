using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MiniAccount.Models
{
    public class VoucherEntry
    {
        public int Id { get; set; }

        public int VoucherId { get; set; }
        [ValidateNever]
        public Voucher Voucher { get; set; }

        public int AccountId { get; set; }
        [ValidateNever]
        public Account Account { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
    }
}
