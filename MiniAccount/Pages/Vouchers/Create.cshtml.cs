// Pages/Vouchers/Create.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccount.Data;
using MiniAccount.Models;
using Microsoft.EntityFrameworkCore;

namespace MiniAccount.Pages.Vouchers
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Voucher Voucher { get; set; }

        [BindProperty]
        public List<VoucherEntry> VoucherEntries { get; set; } = new();

        public List<SelectListItem> AccountList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AccountList = await _context.Accounts
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.AccountName
                }).ToListAsync();

            VoucherEntries.Add(new VoucherEntry());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AccountList = await _context.Accounts
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.AccountName
                }).ToListAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1️⃣ Insert Voucher
            int newVoucherId = await _context.InsertVoucherUsingSPAsync(Voucher);

            // 2️⃣ Insert Each VoucherEntry with that voucher ID
            foreach (var entry in VoucherEntries)
            {
                entry.VoucherId = newVoucherId;
                await _context.InsertVoucherEntryUsingSPAsync(entry);
            }

            return RedirectToPage("Index");
        }
    }
}
