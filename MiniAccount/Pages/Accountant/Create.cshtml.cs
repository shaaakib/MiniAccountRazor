using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccount.Data;
using MiniAccount.Models;

namespace MiniAccount.Pages.Accountant
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Account Account { get; set; }

        public SelectList ParentAccounts { get; set; }

        public void OnGet()
        {
            ParentAccounts = new SelectList(_db.Accounts.ToList(), "Id", "AccountName");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ParentAccounts = new SelectList(_db.Accounts.ToList(), "Id", "AccountName");
                return Page();
            }

            _db.Accounts.Add(Account);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
