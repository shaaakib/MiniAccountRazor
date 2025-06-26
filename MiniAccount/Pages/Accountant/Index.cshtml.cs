using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Data;
using MiniAccount.Models;
using MiniAccount.Models.ViewModel;

namespace MiniAccount.Pages.Accountant
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //public List<Account> Accounts { get; set; }

        //public void OnGet()
        //{
        //    Accounts = _context.Accounts
        //        .Include(a => a.ParentAccount)
        //        .ToList();
        //}

        public List<AccountViewModel> Accounts { get; set; }

        public void OnGet()
        {
            Accounts = _db.AccountViewModels
                .FromSqlRaw("EXEC sp_ManageChartOfAccounts")
                .ToList();
        }
    }
}
