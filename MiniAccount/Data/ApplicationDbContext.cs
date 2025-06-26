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

        // Method to get all Accounts using stored procedure

        public async Task<List<AccountViewModel>> GetAllAccountsUsingSPAsync()
        {
            return await AccountViewModels.FromSqlRaw("EXEC sp_ManageChartOfAccounts").ToListAsync();
        }
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

        public void UpdateAccountUsingSP(Account account)
        {
            Database.ExecuteSqlRaw(
                "EXEC sp_UpdateAccount @p0, @p1, @p2, @p3, @p4, @p5",
                account.Id,
                account.AccountName,
                account.AccountCode,
                account.AccountType,
                account.ParentAccountId,
                account.IsActive
            );
        }

        public void DeleteAccountUsingSP(int id)
        {
            Database.ExecuteSqlRaw("EXEC sp_DeleteAccount @p0", id);
        }

        // Method to get all Vouchers using stored procedure
        public async Task<List<Voucher>> GetAllVouchersUsingSPAsync()
        {
            return await Vouchers.FromSqlRaw("EXEC sp_GetAllVouchers").ToListAsync();
        }
    }
}
