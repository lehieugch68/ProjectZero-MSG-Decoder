using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ProjectZero_MSG_Decoder
{
    public static class XLSX
    {
        public static void ExportXLSX(string[] text0, string[] text1, string[] sub, string file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            
            var workSheet = excel.Workbook.Worksheets.Add("Archive_30");
            workSheet.DefaultRowHeight = 12;
            workSheet.Cells.Style.WrapText = true;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[1, 1].Value = "English";
            workSheet.Cells[1, 2].Value = "Translation";
            int recordIndex = 2;

            foreach (var row in text0)
            {
                workSheet.Cells[recordIndex, 1].Value = row;
                workSheet.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();

            var workSheet1 = excel.Workbook.Worksheets.Add("Archive_5");
            workSheet1.Cells.Style.WrapText = true;
            workSheet1.DefaultRowHeight = 12;
            workSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet1.Cells[1, 1].Value = "English";
            workSheet1.Cells[1, 2].Value = "Translation";
            recordIndex = 2;

            foreach (var row in text1)
            {
                workSheet1.Cells[recordIndex, 1].Value = row;
                workSheet1.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }
            workSheet1.Column(1).AutoFit();
            workSheet1.Column(2).AutoFit();

            var workSheet2 = excel.Workbook.Worksheets.Add("Subtitles");
            workSheet2.Cells.Style.WrapText = true;
            workSheet2.DefaultRowHeight = 12;
            workSheet2.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet2.Cells[1, 1].Value = "English";
            workSheet2.Cells[1, 2].Value = "Translation";
            recordIndex = 2;

            foreach (var row in sub)
            {
                workSheet2.Cells[recordIndex, 1].Value = row;
                workSheet2.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }
            workSheet2.Column(1).AutoFit();
            workSheet2.Column(2).AutoFit();

            File.WriteAllBytes(file, excel.GetAsByteArray());
        }


    }
}
