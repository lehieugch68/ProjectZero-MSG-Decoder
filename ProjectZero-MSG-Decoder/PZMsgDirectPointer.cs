using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectZero_MSG_Decoder
{
    public static class PZMsgDirectPointer
    {
        private static long[] GetPointers(ref BinaryReader reader, long start, long end, out int blockCount)
        {
            reader.BaseStream.Seek(start, SeekOrigin.Begin);
            long endPointerOffset = end < 0 ? reader.BaseStream.Length : end;
            int count = 0;
            while (reader.BaseStream.Position < endPointerOffset)
            {
                int temp = reader.ReadInt32();
                if (temp < endPointerOffset) endPointerOffset = temp;
                count++;
            }
            blockCount = count;
            reader.BaseStream.Seek(start, SeekOrigin.Begin);
            long[] pointers = new long[blockCount];
            for (int i = 0; i < blockCount; i++)
            {
                pointers[i] = reader.ReadInt32();
            }
            return pointers;
        }
        public static string[] GetAllText(string file, long start, long end = -1)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                BinaryReader reader = new BinaryReader(stream);
                int blockCount;
                long[] pointers = GetPointers(ref reader, start, end, out blockCount);
                string[] messages = new string[blockCount];
                for (int i = 0; i < blockCount; i++)
                {
                    reader.BaseStream.Position = pointers[i];
                    long nextPointer = i >= blockCount - 1 ? reader.BaseStream.Length : pointers[i + 1];
                    int strLen = (int)(nextPointer - pointers[i]);
                    byte[] raw = reader.ReadBytes(strLen);
                    messages[i] = GameEncoding.GetString(raw);
                }
                reader.Close();
                return messages;
            }
        }
        public static byte[] Repack(string[] text, string file, out long size, long start, long end = -1)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                MemoryStream result = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(result);
                BinaryReader reader = new BinaryReader(stream);
                writer.Write(reader.ReadBytes((int)start));
                int blockCount;
                long[] pointers = GetPointers(ref reader, start, end, out blockCount);
                writer.Write(new byte[4 * blockCount]);
                reader.BaseStream.Position = pointers.FirstOrDefault();
                for (int i = 0; i < blockCount; i++)
                {
                    long temp = writer.BaseStream.Position;
                    writer.BaseStream.Position = start + (4 * i);
                    writer.Write((int)temp);
                    writer.BaseStream.Position = temp;
                    string msg = i < text.Length ? text[i] : "{End}";
                    byte[] raw = GameEncoding.GetBytes(msg);
                    writer.Write(raw);
                }
                reader.Close();
                size = writer.BaseStream.Length;
                if (writer.BaseStream.Length % 0x800 != 0)
                {
                    int zeroesLen = 0x800 - ((int)writer.BaseStream.Length % 0x800);
                    writer.BaseStream.Position = writer.BaseStream.Length;
                    writer.Write(new byte[zeroesLen]);
                }
                return result.ToArray();
            }
        }
    }
}
