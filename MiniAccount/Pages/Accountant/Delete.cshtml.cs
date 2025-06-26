using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccount.Data;
using MiniAccount.Models;
using MiniAccount.Utility;

namespace MiniAccount.Pages.Accountant
{
    [Authorize(Roles = SD.Role_Admin)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; }

        public IActionResult OnGet(int id)
        {
            Account = _context.Accounts.FirstOrDefault(a => a.Id == id);
            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Account == null || Account.Id == 0)
            {
                return NotFound();
            }

            _context.DeleteAccountUsingSP(Account.Id); // Call stored procedure

            return RedirectToPage("Index");
        }
    }
}
