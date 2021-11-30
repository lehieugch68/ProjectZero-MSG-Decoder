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
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (args.Length > 0)
            {
                FileInfo fileInfo = new FileInfo(args[0]);
                ExcelPackage package = new ExcelPackage(fileInfo);
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Archive_0"];
                int rows = worksheet.Dimension.Rows;
                List<string> text0 = new List<string>();
                for (int i = 2; i <= rows; i++)
                {
                    if (worksheet.Cells[i, 2].Value != null && worksheet.Cells[i, 2].Value.ToString().Length > 0) text0.Add(worksheet.Cells[i, 2].Value.ToString());
                    else text0.Add(worksheet.Cells[i, 1].Value.ToString());
                }
                ExcelWorksheet worksheet1 = package.Workbook.Worksheets["Archive_1"];
                rows = worksheet1.Dimension.Rows;
                List<string> text1 = new List<string>();
                for (int i = 2; i <= rows; i++)
                {
                    if (worksheet1.Cells[i, 2].Value != null && worksheet1.Cells[i, 2].Value.ToString().Length > 0) text1.Add(worksheet1.Cells[i, 2].Value.ToString());
                    else text1.Add(worksheet1.Cells[i, 1].Value.ToString());
                }

                long text0Size, text1Size;

                PZMsg pzMsg = new PZMsg(args[1]);
                byte[] archive0Bytes = pzMsg.Rebuild(text0.ToArray(), out text0Size);

                byte[] archive1Bytes = PZMsgDirectPointer.Repack(text1.ToArray(), args[2], out text1Size, 4974);

                long arc0Pos, arc1Pos;

                using (FileStream stream = File.Open(args[3], FileMode.Open, FileAccess.Write))
                {
                    stream.SetLength(565280 * 2048);
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.BaseStream.Position = writer.BaseStream.Length;
                    arc0Pos = writer.BaseStream.Position / 2048;
                    writer.Write(archive0Bytes);
                    arc1Pos = writer.BaseStream.Position / 2048;
                    writer.Write(archive1Bytes);
                    writer.Close();
                }

                using (FileStream stream = File.Open(args[4], FileMode.Open, FileAccess.Write))
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.BaseStream.Position = 160;
                    writer.Write((int)arc0Pos);
                    writer.Write((int)text0Size);
                    writer.BaseStream.Position = 120;
                    writer.Write((int)arc1Pos);
                    writer.Write((int)text1Size);
                    writer.Close();
                }
            }

            /*string[] text0 = pzMsg.GetAllText();
            string[] text1 = PZMsgDirectPointer.GetAllText(@"D:\Games\Fatal Frame\text0.msg", 4974);
            XLSX.ExportXLSX(text0, text1, @"D:\Games\Fatal Frame\MSG.xlsx");*/
        }

    }
}
