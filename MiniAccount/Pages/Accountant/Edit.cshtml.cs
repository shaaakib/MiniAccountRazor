using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccount.Data;
using MiniAccount.Models;
using MiniAccount.Utility;

namespace MiniAccount.Pages.Accountant
{
    [Authorize(Roles = SD.Role_Admin)]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; }

        public SelectList ParentAccounts { get; set; }

        public IActionResult OnGet(int id)
        {
            Account = _context.Accounts.Find(id);
            if (Account == null)
            {
                return NotFound();
            }

            ParentAccounts = new SelectList(_context.Accounts.Where(a => a.Id != id).ToList(), "Id", "AccountName");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ParentAccounts = new SelectList(_context.Accounts.Where(a => a.Id != Account.Id).ToList(), "Id", "AccountName");
                return Page();
            }

            _context.UpdateAccountUsingSP(Account); // Stored Procedure call

            return RedirectToPage("Index");
        }

    }
}
