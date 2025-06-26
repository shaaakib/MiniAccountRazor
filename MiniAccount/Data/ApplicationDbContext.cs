using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
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

        public async Task<int> InsertVoucherUsingSPAsync(Voucher voucher)
        {
            var voucherIdParam = new SqlParameter
            {
                ParameterName = "@NewVoucherId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await Database.ExecuteSqlRawAsync(
                "EXEC sp_InsertVoucher @VoucherType, @ReferenceNo, @VoucherDate, @NewVoucherId OUTPUT",
                new SqlParameter("@VoucherType", voucher.VoucherType ?? ""),
                new SqlParameter("@ReferenceNo", voucher.ReferenceNo ?? ""),
                new SqlParameter("@VoucherDate", voucher.VoucherDate ?? DateTime.Now),
                voucherIdParam
            );

            return (int)voucherIdParam.Value;
        }

        public async Task InsertVoucherEntryUsingSPAsync(VoucherEntry entry)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC sp_InsertVoucherEntry @VoucherId, @AccountId, @DebitAmount, @CreditAmount",
                new SqlParameter("@VoucherId", entry.VoucherId),
                new SqlParameter("@AccountId", entry.AccountId),
                new SqlParameter("@DebitAmount", entry.DebitAmount ?? 0),
                new SqlParameter("@CreditAmount", entry.CreditAmount ?? 0)
            );
        }
    }
}
