using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Data;
using MiniAccount.Models;

namespace MiniAccount.Pages.Vouchers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Voucher> Vouchers { get; set; }

        public async Task OnGetAsync()
        {
            Vouchers = await _context.Vouchers
                .OrderByDescending(v => v.Id)
                .ToListAsync();
        }
    }
}
