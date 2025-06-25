using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccount.Data;
using MiniAccount.Models;

namespace MiniAccount.Pages.Accountant
{
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
            var acc = _context.Accounts.Find(Account.Id);
            if (acc != null)
            {
                _context.Accounts.Remove(acc);
                _context.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
