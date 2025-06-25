using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccount.Data;
using MiniAccount.Models;

namespace MiniAccount.Pages.Accountant
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; }

        public SelectList ParentAccounts { get; set; }

        public void OnGet()
        {
            ParentAccounts = new SelectList(_context.Accounts.ToList(), "Id", "AccountName");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ParentAccounts = new SelectList(_context.Accounts.ToList(), "Id", "AccountName");
                return Page();
            }

            _context.Accounts.Add(Account);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
