using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Data;
using MiniAccount.Models;
using MiniAccount.Utility;
using System.IO;
using System.Threading.Tasks;

namespace MiniAccount.Pages.Vouchers
{
    [Authorize(Roles = SD.Role_Accountant + "," + SD.Role_Admin)]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Voucher Voucher { get; set; } = new();
        public List<VoucherEntry> Entries { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var results = await _db.GetVoucherDetailsAsync(id);

            if (!results.Any()) return NotFound();

            var first = results.First();

            Voucher.Id = first.VoucherId;
            Voucher.VoucherType = first.VoucherType;
            Voucher.ReferenceNo = first.ReferenceNo;
            Voucher.VoucherDate = first.VoucherDate;

            Entries = results.Select(r => new VoucherEntry
            {
                Id = r.EntryId,
                VoucherId = r.VoucherId,
                AccountId = r.AccountId,
                Account = new Account { Id = r.AccountId, AccountName = r.AccountName },
                DebitAmount = r.DebitAmount,
                CreditAmount = r.CreditAmount
            }).ToList();

            Voucher.Entries = Entries;

            return Page();
        }

        // Handle GET for Excel download
        public async Task<IActionResult> OnGetDownloadExcelAsync(int id)
        {
            var results = await _db.GetVoucherDetailsAsync(id);
            if (!results.Any()) return NotFound();

            var first = results.First();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Voucher");

            worksheet.Cell("A1").Value = "Voucher Details";
            worksheet.Cell("A3").Value = "ID";
            worksheet.Cell("B3").Value = first.VoucherId;
            worksheet.Cell("A4").Value = "Type";
            worksheet.Cell("B4").Value = first.VoucherType;
            worksheet.Cell("A5").Value = "Reference";
            worksheet.Cell("B5").Value = first.ReferenceNo;
            worksheet.Cell("A6").Value = "Date";
            worksheet.Cell("B6").Value = first.VoucherDate?.ToString("yyyy-MM-dd");

            worksheet.Cell("A8").Value = "Account";
            worksheet.Cell("B8").Value = "Debit";
            worksheet.Cell("C8").Value = "Credit";

            int row = 9;
            foreach (var r in results)
            {
                worksheet.Cell(row, 1).Value = r.AccountName;
                worksheet.Cell(row, 2).Value = r.DebitAmount ?? 0;
                worksheet.Cell(row, 3).Value = r.CreditAmount ?? 0;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            string filename = $"Voucher_{first.VoucherId}.xlsx";
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }
    }
}
