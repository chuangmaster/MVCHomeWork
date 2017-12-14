using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Web;

namespace MVCHomeWork.Service
{
    public class ExcelExportService
    {
        public static string Export(string Path, string sheetName, List<string> headers, IEnumerable<object> data)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);
            var column = 1;
            foreach (var h in headers)
            {
                worksheet.Cell(1, column++).Value = h;
            }
            worksheet.Cell(2, 1).Value = data;
            var fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            workbook.SaveAs(string.Format($"{Path}/{fileName}.xlsx"));
            return string.Format($"{Path}/{fileName}.xlsx");
        }
    }
}