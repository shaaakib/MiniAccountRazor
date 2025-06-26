using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Models;

namespace MiniAccount.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherEntry> VoucherEntries { get; set; }
    }
}
