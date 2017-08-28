using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire
{
    class KrosmagaReader
    {
        private BinaryReader b;

        public BinaryReader B
        {
            get
            {
                return b;
            }

            private set
            {
                b = value;
            }
        }

        public void SetData(byte[] data)
        {
            Stream f = new MemoryStream(data);
            B = new BinaryReader(f);
        }

        public int ReadTag()
        {
            return (int)B.ReadByte();
        }

        public ulong ReadRawVarint64()
        {
            int i = 0;
            ulong num = 0uL;
            while (i < 64)
            {
                byte bb = B.ReadByte();
                num |= (ulong)((ulong)((long)(bb & 127)) << i);
                if ((bb & 128) == 0)
                {
                    return num;
                }
                i += 7;
            }
            return 0;
        }

        public uint ReadRawVarint32()
        {
            if (B.BaseStream.Position + 5 > B.BaseStream.Length)
            {
                return SlowReadRawVarint32();
            }
            int num = (int)B.ReadByte();
            if (num < 128)
            {
                return (uint)num;
            }
            int num2 = num & 127;
            if ((num = (int)B.ReadByte()) < 128)
            {
                num2 |= num << 7;
            }
            else
            {
                num2 |= (num & 127) << 7;
                if ((num = (int)B.ReadByte()) < 128)
                {
                    num2 |= num << 14;
                }
                else
                {
                    num2 |= (num & 127) << 14;
                    if ((num = (int)B.ReadByte()) < 128)
                    {
                        num2 |= num << 21;
                    }
                    else
                    {
                        num2 |= (num & 127) << 21;
                        num2 |= (num = (int)B.ReadByte()) << 28;
                        if (num >= 128)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (ReadRawByte() < 128)
                                {
                                    return (uint)num2;
                                }
                            }
                        }
                    }
                }
            }
            return (uint)num2;
        }

        public uint SlowReadRawVarint32()
        {
            int num = (int)ReadRawByte();
            if (num < 128)
            {
                return (uint)num;
            }
            int num2 = num & 127;
            if ((num = (int)ReadRawByte()) < 128)
            {
                num2 |= num << 7;
            }
            else
            {
                num2 |= (num & 127) << 7;
                if ((num = (int)ReadRawByte()) < 128)
                {
                    num2 |= num << 14;
                }
                else
                {
                    num2 |= (num & 127) << 14;
                    if ((num = (int)ReadRawByte()) < 128)
                    {
                        num2 |= num << 21;
                    }
                    else
                    {
                        num2 |= (num & 127) << 21;
                        num2 |= (num = (int)ReadRawByte()) << 28;
                        if (num >= 128)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (ReadRawByte() < 128)
                                {
                                    return (uint)num2;
                                }
                            }
                        }
                    }
                }
            }
            return (uint)num2;
        }

        public byte ReadRawByte()
        {
            if (B.BaseStream.Position == B.BaseStream.Length)
                throw new Exception("Erreur de lecture");
            return B.ReadByte();
        }

        public string ReadString()
        {
            int num = (int)ReadRawVarint32();
            if (num == 0)
            {
                return "";
            }
            return Encoding.UTF8.GetString(b.ReadBytes(num));
        }

        public bool ReadBool()
        {
            return (ReadRawVarint32() != 0);
        }

        public int DecodeZigZag32(uint n)
        {
            return (int)(n >> 1 ^ -(int)(n & 1));
        }

        public byte[] ReadMessage(int size)
        {
            return b.ReadBytes(size);
        }

        public ulong ReadRawLittleEndian64()
        {
            ulong num = (ulong)ReadRawByte();
            ulong num2 = (ulong)ReadRawByte();
            ulong num3 = (ulong)ReadRawByte();
            ulong num4 = (ulong)ReadRawByte();
            ulong num5 = (ulong)ReadRawByte();
            ulong num6 = (ulong)ReadRawByte();
            ulong num7 = (ulong)ReadRawByte();
            ulong num8 = (ulong)ReadRawByte();

            return num | num2 << 8 | num3 << 16 | num4 << 24 | num5 << 32 | num6 << 40 | num7 << 48 | num8 << 56;
        }
    }
}
