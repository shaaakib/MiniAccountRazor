using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Data;
using MiniAccount.Models;

namespace MiniAccount.Pages.Accountant
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Account> Accounts { get; set; }

        public void OnGet()
        {
            Accounts = _context.Accounts
                .Include(a => a.ParentAccount)
                .ToList();
        }
    }
}
