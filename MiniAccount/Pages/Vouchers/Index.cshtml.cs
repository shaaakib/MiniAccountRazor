using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Data;
using MiniAccount.Models;

namespace MiniAccount.Pages.Vouchers
{
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
