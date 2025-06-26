using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAccount.Data;
using MiniAccount.Models;
using ClosedXML.Excel;
using System.IO;
using System.Threading.Tasks;

namespace MiniAccount.Pages.Vouchers
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Voucher Voucher { get; set; }

        // Handle GET for View page
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Voucher = await _context.Vouchers
                .Include(v => v.Entries)
                .ThenInclude(e => e.Account)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (Voucher == null)
                return NotFound();

            return Page();
        }

        // Handle GET for Excel download
        public async Task<IActionResult> OnGetDownloadExcelAsync(int id)
        {
            var voucher = await _context.Vouchers
                .Include(v => v.Entries)
                .ThenInclude(e => e.Account)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (voucher == null)
                return NotFound();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Voucher");

            worksheet.Cell("A1").Value = "Voucher Details";
            worksheet.Cell("A3").Value = "ID";
            worksheet.Cell("B3").Value = voucher.Id;
            worksheet.Cell("A4").Value = "Type";
            worksheet.Cell("B4").Value = voucher.VoucherType;
            worksheet.Cell("A5").Value = "Reference";
            worksheet.Cell("B5").Value = voucher.ReferenceNo;
            worksheet.Cell("A6").Value = "Date";
            worksheet.Cell("B6").Value = voucher.VoucherDate?.ToString("yyyy-MM-dd");

            worksheet.Cell("A8").Value = "Account";
            worksheet.Cell("B8").Value = "Debit";
            worksheet.Cell("C8").Value = "Credit";

            int row = 9;
            foreach (var entry in voucher.Entries)
            {
                worksheet.Cell(row, 1).Value = entry.Account.AccountName;
                worksheet.Cell(row, 2).Value = entry.DebitAmount ?? 0;
                worksheet.Cell(row, 3).Value = entry.CreditAmount ?? 0;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            string filename = $"Voucher_{voucher.Id}.xlsx";
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }
    }
}
