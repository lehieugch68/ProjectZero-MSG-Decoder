using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectZero_MSG_Decoder
{
    public class PZMsg
    {
        public class BlockText
        {
            public int Index;
            public int PointerOffset;
            public PZMessage[] Messages;
            public BlockText(int index, int offset)
            {
                this.Index = index;
                this.PointerOffset = offset;
            }
        }
        public class PZMessage
        {
            public int Index;
            public int PointerOffset;
            public string Message;
            public PZMessage(int index, int offset)
            {
                this.Index = index;
                this.PointerOffset = offset;
            }
        }
        public BlockText[] _AllBlocks;
        public BlockText[] _SubBlocks;
        public string _File;
        public PZMsg(string file)
        {
            _File = file;
        }
        private PZMessage[] GetBlockStrings(ref BinaryReader reader, BlockText block, int endPointerOffset)
        {
            int strCount = 0;
            int endBlock = endPointerOffset;
            while (reader.BaseStream.Position < endPointerOffset)
            {
                int temp = reader.ReadInt32();
                if (temp < endPointerOffset) endPointerOffset = temp;
                strCount++;
            }
            reader.BaseStream.Seek(block.PointerOffset, SeekOrigin.Begin);
            List<PZMessage> msgs = new List<PZMessage>();
            for (int i = 0; i < strCount; i++)
            {
                int pointerOffset = reader.ReadInt32();
                PZMessage msg = new PZMessage(i, pointerOffset);
                msgs.Add(msg);
            }
            PZMessage[] msgsSorted = msgs.OrderBy(e => e.PointerOffset).ToArray();
            for (int i = 0; i < msgsSorted.Length; i++)
            {
                int endOffset = i >= msgsSorted.Length - 1 ? endBlock : msgsSorted[i + 1].PointerOffset;
                int strLen = endOffset - msgsSorted[i].PointerOffset;
                reader.BaseStream.Seek(msgsSorted[i].PointerOffset, SeekOrigin.Begin);
                byte[] raw = reader.ReadBytes(strLen);
                msgsSorted[i].Message = GameEncoding.GetString(raw);
            }
            return msgs.ToArray();
        }
        private BlockText[] GetBlocks(byte[] input, long start = 0, long end = -1)
        {
            using (MemoryStream stream = new MemoryStream(input))
            {
                BinaryReader reader = new BinaryReader(stream);
                reader.BaseStream.Seek(start, SeekOrigin.Begin);
                if (end == -1) end = (int)reader.BaseStream.Length;
                int endPointerOffset = (int)end;
                int blockCount = 0;
                while (reader.BaseStream.Position < endPointerOffset)
                {
                    int temp = reader.ReadInt32();
                    if (temp < endPointerOffset) endPointerOffset = temp;
                    blockCount++;
                }
                reader.BaseStream.Seek(start, SeekOrigin.Begin);
                List<BlockText> blocks = new List<BlockText>();
                for (int i = 0; i < blockCount; i++)
                {
                    int pointerOffset = reader.ReadInt32();
                    BlockText block = new BlockText(i, pointerOffset);
                    blocks.Add(block);
                }
                BlockText[] blocksSorted = blocks.OrderBy(e => e.PointerOffset).ToArray();
                for (int i = 0; i < blocksSorted.Length; i++)
                {
                    reader.BaseStream.Seek(blocksSorted[i].PointerOffset, SeekOrigin.Begin);
                    if (i == 31 && start == 0)
                    {
                        BlockText[] subBlocks = GetBlocks(ref reader, blocksSorted[i].PointerOffset, blocksSorted[i + 1].PointerOffset);
                        _SubBlocks = subBlocks;
                        continue;
                    }
                    int endOffset = i >= blocksSorted.Length - 1 ? (int)end : blocksSorted[i + 1].PointerOffset;
                    blocksSorted[i].Messages = i >= blocksSorted.Length - 1 ? blocksSorted[i].Messages = GetBlockStringsDirectPointer(ref reader, blocksSorted[i], endOffset) :
                        blocksSorted[i].Messages = GetBlockStrings(ref reader, blocksSorted[i], endOffset);
                }
                reader.Close();
                return blocksSorted;
            }
        }
        private long[] GetDirectPointers(ref BinaryReader reader, long startPointerOffset, long endPointerOffset)
        {
            int strCount = 0;
            while (reader.BaseStream.Position < endPointerOffset)
            {
                int temp = reader.ReadInt32();
                if (temp < endPointerOffset) endPointerOffset = temp;
                strCount++;
            }
            reader.BaseStream.Seek(startPointerOffset, SeekOrigin.Begin);
            long[] pointers = new long[strCount];
            for (int i = 0; i < strCount; i++)
            {
                pointers[i] = reader.ReadInt32();
            }
            return pointers;
        }
        private PZMessage[] GetBlockStringsDirectPointer(ref BinaryReader reader, BlockText block, int endPointerOffset)
        {
            long[] pointers = GetDirectPointers(ref reader, block.PointerOffset, endPointerOffset);
            List<PZMessage> messages = new List<PZMessage>();
            for (int i = 0; i < pointers.Length; i++)
            {
                reader.BaseStream.Position = pointers[i];
                long nextPointer = i >= pointers.Length - 1 ? reader.BaseStream.Length : pointers[i + 1];
                int strLen = (int)(nextPointer - pointers[i]);
                byte[] raw = reader.ReadBytes(strLen);
                PZMessage msg = new PZMessage(i, (int)pointers[i]);
                msg.Message = GameEncoding.GetString(raw);
                messages.Add(msg);
            }
            return messages.ToArray();
        }
        private BlockText[] GetBlocks(ref BinaryReader reader, long start = 0, long end = -1)
        {
            reader.BaseStream.Seek(start, SeekOrigin.Begin);
            if (end == -1) end = (int)reader.BaseStream.Length;
            int endPointerOffset = (int)end;
            int blockCount = 0;
            while (reader.BaseStream.Position < endPointerOffset)
            {
                int temp = reader.ReadInt32();
                if (temp < endPointerOffset) endPointerOffset = temp;
                blockCount++;
            }
            reader.BaseStream.Seek(start, SeekOrigin.Begin);

            List<BlockText> blocks = new List<BlockText>();
            for (int i = 0; i < blockCount; i++)
            {
                int pointerOffset = reader.ReadInt32();
                BlockText block = new BlockText(i, pointerOffset);
                blocks.Add(block);
            }
            BlockText[] blocksSorted = blocks.OrderBy(e => e.PointerOffset).ToArray();
            for (int i = 0; i < blocksSorted.Length; i++)
            {
                reader.BaseStream.Seek(blocksSorted[i].PointerOffset, SeekOrigin.Begin);
                int endOffset = i >= blocksSorted.Length - 1 ? (int)end : blocksSorted[i + 1].PointerOffset;
                blocksSorted[i].Messages = GetBlockStrings(ref reader, blocksSorted[i], endOffset);
            }
            return blocksSorted;
        }
        public BlockText GetBlock(ref BinaryReader reader, long start = 0, long end = -1)
        {
            BlockText block = new BlockText(0, (int)start);
            reader.BaseStream.Seek(start, SeekOrigin.Begin);
            int strCount = 0;
            if (end == -1) end = reader.BaseStream.Length;
            long endTemp = end;
            while (reader.BaseStream.Position < endTemp)
            {
                int temp = reader.ReadInt32();
                if (temp < endTemp) endTemp = temp;
                strCount++;
            }
            reader.BaseStream.Seek(block.PointerOffset, SeekOrigin.Begin);
            List<PZMessage> msgs = new List<PZMessage>();
            for (int i = 0; i < strCount; i++)
            {
                int pointerOffset = reader.ReadInt32();
                PZMessage msg = new PZMessage(i, pointerOffset);
                msgs.Add(msg);
            }
            PZMessage[] msgsSorted = msgs.OrderBy(e => e.PointerOffset).ToArray();
            for (int i = 0; i < msgsSorted.Length; i++)
            {
                int endOffset = i >= msgsSorted.Length - 1 ? (int)end : msgsSorted[i + 1].PointerOffset;
                int strLen = endOffset - msgsSorted[i].PointerOffset;
                reader.BaseStream.Seek(msgsSorted[i].PointerOffset, SeekOrigin.Begin);
                byte[] raw = reader.ReadBytes(strLen);
                msgsSorted[i].Message = GameEncoding.GetString(raw);
                //Console.WriteLine(msgsSorted[i].Message);

            }
            block.Messages = msgsSorted;
            return block;
        }
        public void GetAllBlock()
        {
            byte[] input = File.ReadAllBytes(_File);
            _AllBlocks = GetBlocks(input);
        }
        public string[] GetAllText()
        {
            if (_AllBlocks == null) GetAllBlock();
            List<string> allText = new List<string>();
            //Console.WriteLine(_AllBlocks[_AllBlocks.Length - 1].PointerOffset);
            foreach (BlockText block in _AllBlocks)
            {
                if (block.Messages == null)
                {
                    foreach (BlockText subBlock in _SubBlocks)
                    {
                        foreach (PZMessage msg in subBlock.Messages)
                        {
                            allText.Add(msg.Message);
                        }
                    }
                }
                else
                {
                    foreach (PZMessage msg in block.Messages)
                    {
                        allText.Add(msg.Message);
                    }
                }    
            }
            return allText.ToArray();
        }
        public byte[] Rebuild(string[] lines, out long size, bool align = true)
        {
            int index = 0;
            if (_AllBlocks == null) GetAllBlock();
            for (int i = 0; i < _AllBlocks.Length; i++)
            {
                if (_AllBlocks[i].Messages == null)
                {
                    for (int x = 0; x < _SubBlocks.Length; x++)
                    {
                        for (int y = 0; y < _SubBlocks[x].Messages.Length; y++)
                        {
                            if (index <= lines.Length) _SubBlocks[x].Messages[y].Message = lines[index++];
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < _AllBlocks[i].Messages.Length; x++)
                    {
                        if (index <= lines.Length) _AllBlocks[i].Messages[x].Message = lines[index++];
                    }    
                }    
            }
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(new byte[_AllBlocks.Length * 4]);
            for (int i = 0; i < _AllBlocks.Length; i++)
            {
                if (_AllBlocks[i].Messages == null)
                {
                    _AllBlocks[i].PointerOffset = (int)writer.BaseStream.Position;
                    writer.Write(new byte[_SubBlocks.Length * 4]);
                    for (int s = 0; s < _SubBlocks.Length; s++)
                    {
                        _SubBlocks[s].PointerOffset = (int)writer.BaseStream.Position;
                        writer.Write(new byte[_SubBlocks[s].Messages.Length * 4]);
                        for (int a = 0; a < _SubBlocks[s].Messages.Length; a++)
                        {
                            _SubBlocks[s].Messages[a].PointerOffset = (int)writer.BaseStream.Position;
                            byte[] msgBytes = GameEncoding.GetBytes(_SubBlocks[s].Messages[a].Message);
                            writer.Write(msgBytes);
                        }
                        long temp = writer.BaseStream.Position;
                        writer.BaseStream.Position = _SubBlocks[s].PointerOffset;
                        _SubBlocks[s].Messages = _SubBlocks[s].Messages.OrderBy(entry => entry.Index).ToArray();
                        foreach (PZMessage entry in _SubBlocks[s].Messages)
                        {
                            writer.Write(entry.PointerOffset);
                        }
                        writer.BaseStream.Position = temp;
                    }
                    long tempPos = writer.BaseStream.Position;
                    writer.BaseStream.Position = _AllBlocks[i].PointerOffset;
                    _SubBlocks = _SubBlocks.OrderBy(entry => entry.Index).ToArray();
                    foreach (BlockText block in _SubBlocks)
                    {
                        writer.Write(block.PointerOffset);
                    }
                    writer.BaseStream.Position = tempPos;
                }
                else if (i == _AllBlocks.Length - 1)
                {
                    _AllBlocks[i].PointerOffset = (int)writer.BaseStream.Position;
                    writer.Write(new byte[_AllBlocks[i].Messages.Length * 4]);
                    for (int x = 0; x < _AllBlocks[i].Messages.Length; x++)
                    {
                        _AllBlocks[i].Messages[x].PointerOffset = (int)writer.BaseStream.Position;
                        byte[] msgBytes = GameEncoding.GetBytes(_AllBlocks[i].Messages[x].Message);
                        writer.Write(msgBytes);
                        writer.BaseStream.Position = _AllBlocks[i].PointerOffset + (x * 4);
                        writer.Write(_AllBlocks[i].Messages[x].PointerOffset);
                        writer.BaseStream.Position = writer.BaseStream.Length;
                    }
                }
                else
                {
                    _AllBlocks[i].PointerOffset = (int)writer.BaseStream.Position;
                    writer.Write(new byte[_AllBlocks[i].Messages.Length * 4]);
                    for (int x = 0; x < _AllBlocks[i].Messages.Length; x++)
                    {
                        _AllBlocks[i].Messages[x].PointerOffset = (int)writer.BaseStream.Position;
                        byte[] msgBytes = GameEncoding.GetBytes(_AllBlocks[i].Messages[x].Message);
                        writer.Write(msgBytes);
                    }
                    long temp = writer.BaseStream.Position;
                    writer.BaseStream.Position = _AllBlocks[i].PointerOffset;
                    _AllBlocks[i].Messages = _AllBlocks[i].Messages.OrderBy(entry => entry.Index).ToArray();
                    foreach (PZMessage entry in _AllBlocks[i].Messages)
                    {
                        writer.Write(entry.PointerOffset);
                    }
                    writer.BaseStream.Position = temp;
                }    
            }
            writer.BaseStream.Position = 0;
            _AllBlocks = _AllBlocks.OrderBy(entry => entry.Index).ToArray();
            foreach (BlockText block in _AllBlocks)
            {
                writer.Write(block.PointerOffset);
            }
            size = writer.BaseStream.Length;
            if (align && writer.BaseStream.Length % 0x800 != 0)
            {
                int zeroesLen = 0x800 - ((int)writer.BaseStream.Length % 0x800);
                writer.BaseStream.Position = writer.BaseStream.Length;
                writer.Write(new byte[zeroesLen]);
            }
            writer.Close();
            return stream.ToArray();
        }
    }
}
