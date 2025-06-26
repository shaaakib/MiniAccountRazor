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

            _db.CreateAccountUsingSP(Account); // Stored Procedure call

            return RedirectToPage("Index");
        }
    }
}
