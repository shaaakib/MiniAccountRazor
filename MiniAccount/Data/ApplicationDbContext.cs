using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Models;
using MiniAccount.Models.ViewModel;

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
        public DbSet<AccountViewModel> AccountViewModels { get; set; }

        public void CreateAccountUsingSP(Account account)
        {
            Database.ExecuteSqlRaw(
                "EXEC sp_CreateAccount @p0, @p1, @p2, @p3, @p4",
                account.AccountName,
                account.AccountCode,
                account.AccountType,
                account.ParentAccountId,
                account.IsActive
            );
        }
    }
}
