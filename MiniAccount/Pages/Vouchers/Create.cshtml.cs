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
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"{entry.Key} => {error.ErrorMessage}");
                    }
                }

                return Page();
            }

            Voucher.Entries = VoucherEntries;

            _context.Vouchers.Add(Voucher);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index"); // বা Success পেজে যাও
        }
    }
}
