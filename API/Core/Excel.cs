using API.Contex;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class Excel
    {
        DB003 context = new DB003();

        public byte[] Rent(long pgId, long monthId, int year)
        {
            var rent = context.Rents
                .Include(i => i.Tenant)
                .Include(i => i.PaymentStatus)
                .Include(i => i.Month)
                .Where(w => w.PGID == pgId && w.MonthID == monthId && w.Year == year)
                .ToList();



            if (rent.Count > 0)
            {

                string fileName = "RentDetails_" + rent.First().Month.MonthName + "_" + rent.First().Year + ".xlxs";

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Rent");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "Room No.";
                    worksheet.Cell(currentRow, 2).Value = "Tenant Name";
                    worksheet.Cell(currentRow, 3).Value = "Mobile No.";
                    worksheet.Cell(currentRow, 4).Value = "Rent (Rs)";
                    worksheet.Cell(currentRow, 5).Value = "Payment";
                    worksheet.Cell(currentRow, 6).Value = "Month";
                    worksheet.Cell(currentRow, 7).Value = "Year";

                    foreach (var r in rent)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = r.RoomNo;
                        worksheet.Cell(currentRow, 2).Value = r.FullName;
                        worksheet.Cell(currentRow, 3).Value = r.Tenant.MobileNo;
                        worksheet.Cell(currentRow, 4).Value = r.RentAmount;
                        worksheet.Cell(currentRow, 5).Value = r.PaymentStatus.Status;
                        worksheet.Cell(currentRow, 6).Value = r.Month.MonthName;
                        worksheet.Cell(currentRow, 7).Value = r.Year;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        
                        return content;
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
