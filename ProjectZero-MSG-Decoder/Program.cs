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
            //Tested on SLES_508.21
            //Input: ProjectZero-MSG-Decoder.exe MSG.XLSX Archive_30 Archive_5 Archive_10 Archive_15 Archive_20 Archive_25 IMG_BD.BIN IMG_HD.BIN
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (args.Length > 0)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(args[0]);
                    ExcelPackage package = new ExcelPackage(fileInfo);
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Archive_30"];
                    int rows = worksheet.Dimension.Rows;
                    List<string> text0 = new List<string>();
                    for (int i = 2; i <= rows; i++)
                    {
                        if (worksheet.Cells[i, 2].Value != null && worksheet.Cells[i, 2].Value.ToString().Length > 0) text0.Add(worksheet.Cells[i, 2].Value.ToString());

                        else text0.Add(worksheet.Cells[i, 1].Value != null ? worksheet.Cells[i, 1].Value.ToString() : "{End}");
                    }
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets["Archive_5"];
                    rows = worksheet1.Dimension.Rows;
                    List<string> text1 = new List<string>();
                    for (int i = 2; i <= rows; i++)
                    {
                        if (worksheet1.Cells[i, 2].Value != null && worksheet1.Cells[i, 2].Value.ToString().Trim().Length > 0) text1.Add(worksheet1.Cells[i, 2].Value.ToString());
                        else text1.Add(worksheet1.Cells[i, 1].Value.ToString());

                    }
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets["Archive_10"];
                    rows = worksheet2.Dimension.Rows;
                    List<string> text2 = new List<string>();
                    for (int i = 2; i <= rows; i++)
                    {
                        if (worksheet2.Cells[i, 2].Value != null && worksheet2.Cells[i, 2].Value.ToString().Trim().Length > 0) text2.Add(worksheet2.Cells[i, 2].Value.ToString());
                        else text2.Add(worksheet2.Cells[i, 1].Value.ToString());

                    }
                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets["Archive_15"];
                    rows = worksheet3.Dimension.Rows;
                    List<string> text3 = new List<string>();
                    for (int i = 2; i <= rows; i++)
                    {
                        if (worksheet3.Cells[i, 2].Value != null && worksheet3.Cells[i, 2].Value.ToString().Trim().Length > 0) text3.Add(worksheet3.Cells[i, 2].Value.ToString());
                        else text3.Add(worksheet3.Cells[i, 1].Value.ToString());

                    }
                    ExcelWorksheet worksheet4 = package.Workbook.Worksheets["Archive_20"];
                    rows = worksheet4.Dimension.Rows;
                    List<string> text4 = new List<string>();
                    for (int i = 2; i <= rows; i++)
                    {
                        if (worksheet4.Cells[i, 2].Value != null && worksheet4.Cells[i, 2].Value.ToString().Trim().Length > 0) text4.Add(worksheet4.Cells[i, 2].Value.ToString());
                        else text4.Add(worksheet4.Cells[i, 1].Value.ToString());

                    }
                    ExcelWorksheet worksheet5 = package.Workbook.Worksheets["Archive_25"];
                    rows = worksheet5.Dimension.Rows;
                    List<string> text5 = new List<string>();
                    for (int i = 2; i <= rows; i++)
                    {
                        if (worksheet5.Cells[i, 2].Value != null && worksheet5.Cells[i, 2].Value.ToString().Trim().Length > 0) text5.Add(worksheet5.Cells[i, 2].Value.ToString());
                        else text5.Add(worksheet5.Cells[i, 1].Value.ToString());

                    }

                    long text0Size, text1Size, text2Size, text3Size, text4Size, text5Size;

                    PZMsg pzMsg = new PZMsg(args[1]);
                    byte[] archive0Bytes = pzMsg.Rebuild(text0.ToArray(), out text0Size, true);

                    byte[] archive1Bytes = PZMsgDirectPointer.Repack(text1.ToArray(), args[2], out text1Size, 4970);

                    byte[] archive2Bytes = PZMsgDirectPointer.Repack(text2.ToArray(), args[3], out text2Size, 8828);

                    byte[] archive3Bytes = PZMsgDirectPointer.Repack(text3.ToArray(), args[4], out text3Size, 9616);

                    byte[] archive4Bytes = PZMsgDirectPointer.Repack(text4.ToArray(), args[5], out text4Size, 8328);

                    byte[] archive5Bytes = PZMsgDirectPointer.Repack(text5.ToArray(), args[6], out text5Size, 5342);

                    long arc0Pos, arc1Pos, arc2Pos, arc3Pos, arc4Pos, arc5Pos;

                    //File.WriteAllLines(@"D:\VietHoaGame\Fatal Frame\30.txt", text0.Select(e => e.Replace("\n", "{LF}")).ToArray());

                    using (FileStream stream = File.Open(args[7], FileMode.Open, FileAccess.Write))
                    {
                        stream.SetLength(608976 * 2048);
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.BaseStream.Position = writer.BaseStream.Length;
                        arc0Pos = writer.BaseStream.Position / 2048;
                        writer.Write(archive0Bytes);
                        arc1Pos = writer.BaseStream.Position / 2048;
                        writer.Write(archive1Bytes);
                        arc2Pos = writer.BaseStream.Position / 2048;
                        writer.Write(archive2Bytes);
                        arc3Pos = writer.BaseStream.Position / 2048;
                        writer.Write(archive3Bytes);
                        arc4Pos = writer.BaseStream.Position / 2048;
                        writer.Write(archive4Bytes);
                        arc5Pos = writer.BaseStream.Position / 2048;
                        writer.Write(archive5Bytes);
                        writer.Close();
                    }

                    using (FileStream stream = File.Open(args[8], FileMode.Open, FileAccess.Write))
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.BaseStream.Position = 320;
                        writer.Write((int)arc0Pos);
                        writer.Write((int)text0Size);
                        writer.BaseStream.Position = 120;
                        writer.Write((int)arc1Pos);
                        writer.Write((int)text1Size);
                        writer.BaseStream.Position = 160;
                        writer.Write((int)arc2Pos);
                        writer.Write((int)text2Size);
                        writer.BaseStream.Position = 200;
                        writer.Write((int)arc3Pos);
                        writer.Write((int)text3Size);
                        writer.BaseStream.Position = 240;
                        writer.Write((int)arc4Pos);
                        writer.Write((int)text4Size);
                        writer.BaseStream.Position = 280;
                        writer.Write((int)arc5Pos);
                        writer.Write((int)text5Size);
                        writer.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }



            /*PZMsg pzMsg = new PZMsg(@"D:\VietHoaGame\Fatal Frame\Unpack\30");
            string[] text0 = pzMsg.GetAllText();
            //File.WriteAllLines(@"D:\VietHoaGame\Fatal Frame\30_test.txt", text0);

            string[] text1 = PZMsgDirectPointer.GetAllText(@"D:\VietHoaGame\Fatal Frame\Unpack\5", 4970);
            string[] text2 = PZMsgDirectPointer.GetAllText(@"D:\VietHoaGame\Fatal Frame\Unpack\10", 8828);
            
            string[] text3 = PZMsgDirectPointer.GetAllText(@"D:\VietHoaGame\Fatal Frame\Unpack\15", 9616);
            string[] text4 = PZMsgDirectPointer.GetAllText(@"D:\VietHoaGame\Fatal Frame\Unpack\20", 8328);
            string[] text5 = PZMsgDirectPointer.GetAllText(@"D:\VietHoaGame\Fatal Frame\Unpack\25", 5342);
            XLSX.ExportXLSX(text0, text1, text2, text3, text4, text5, @"D:\VietHoaGame\Fatal Frame\Fatal Frame PS2.xlsx");
            //foreach (var entry in text5) Console.WriteLine(entry);
            Console.ReadKey();*/

            /*PZMsg pzMsg = new PZMsg(@"D:\VietHoaGame\Fatal Frame\Unpack\31");
            string[] text0 = pzMsg.GetAllText();
            File.WriteAllLines(@"D:\VietHoaGame\Fatal Frame\31_test.txt", text0);*/
        }

    }
}
