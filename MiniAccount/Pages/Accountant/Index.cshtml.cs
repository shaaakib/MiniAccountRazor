using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Data;
using MiniAccount.Models;
using MiniAccount.Models.ViewModel;
using MiniAccount.Utility;
using System.Threading.Tasks;

namespace MiniAccount.Pages.Accountant
{
    [Authorize(Roles = SD.Role_Admin)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<AccountViewModel> Accounts { get; set; }

        public async Task OnGetAsync()
        {
            Accounts = await _db.GetAllAccountsUsingSPAsync();
        }
    }
}
