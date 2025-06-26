using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Data;
using MiniAccount.Models;
using MiniAccount.Utility;

namespace MiniAccount.Pages.Vouchers
{
    [Authorize(Roles = SD.Role_Accountant + "," + SD.Role_Admin)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Voucher> Vouchers { get; set; }

        public async Task OnGetAsync()
        {
            Vouchers = await _db.GetAllVouchersUsingSPAsync();
        }
    }
}
