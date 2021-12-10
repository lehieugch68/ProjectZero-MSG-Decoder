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
        public static void ExportXLSX(string[] text0, string[] text1, string[] text2, string[] text3, string[] text4, string[] text5, string file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            
            var workSheet = excel.Workbook.Worksheets.Add("Archive_30");
            workSheet.Cells[1, 1].Value = "English";
            workSheet.Cells[1, 2].Value = "Translation";
            int recordIndex = 2;

            foreach (var row in text0)
            {
                workSheet.Cells[recordIndex, 1].Value = row;
                workSheet.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }

            var workSheet1 = excel.Workbook.Worksheets.Add("Archive_5");
            workSheet1.Cells[1, 1].Value = "English";
            workSheet1.Cells[1, 2].Value = "Translation";
            recordIndex = 2;

            foreach (var row in text1)
            {
                workSheet1.Cells[recordIndex, 1].Value = row;
                workSheet1.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }

            var workSheet2 = excel.Workbook.Worksheets.Add("Archive_10");
            workSheet1.Cells[1, 1].Value = "English";
            workSheet1.Cells[1, 2].Value = "Translation";
            recordIndex = 2;

            foreach (var row in text2)
            {
                workSheet2.Cells[recordIndex, 1].Value = row;
                workSheet2.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }

            var workSheet3 = excel.Workbook.Worksheets.Add("Archive_15");
            workSheet3.Cells[1, 1].Value = "English";
            workSheet3.Cells[1, 2].Value = "Translation";
            recordIndex = 2;

            foreach (var row in text3)
            {
                workSheet3.Cells[recordIndex, 1].Value = row;
                workSheet3.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }

            var workSheet4 = excel.Workbook.Worksheets.Add("Archive_20");
            workSheet4.Cells[1, 1].Value = "English";
            workSheet4.Cells[1, 2].Value = "Translation";
            recordIndex = 2;

            foreach (var row in text4)
            {
                workSheet4.Cells[recordIndex, 1].Value = row;
                workSheet4.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }

            var workSheet5 = excel.Workbook.Worksheets.Add("Archive_25");
            workSheet5.Cells[1, 1].Value = "English";
            workSheet5.Cells[1, 2].Value = "Translation";
            recordIndex = 2;

            foreach (var row in text5)
            {
                workSheet5.Cells[recordIndex, 1].Value = row;
                workSheet5.Cells[recordIndex, 2].Value = "";
                recordIndex++;
            }

            File.WriteAllBytes(file, excel.GetAsByteArray());
        }


    }
}
