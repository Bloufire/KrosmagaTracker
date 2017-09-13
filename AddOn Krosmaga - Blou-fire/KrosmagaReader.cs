using System;
using System.IO;
using System.Text;

namespace AddOn_Krosmaga___Blou_fire
{
	internal class KrosmagaReader
	{
		public BinaryReader B { get; private set; }

		public void SetData(byte[] data)
		{
			Stream f = new MemoryStream(data);
			B = new BinaryReader(f);
		}

		public int ReadTag()
		{
			return B.ReadByte();
		}

		public ulong ReadRawVarint64()
		{
			var i = 0;
			var num = 0uL;
			while (i < 64)
			{
				var bb = B.ReadByte();
				num |= (ulong) (bb & 127) << i;
				if ((bb & 128) == 0)
					return num;
				i += 7;
			}
			return 0;
		}

		public uint ReadRawVarint32()
		{
			if (B.BaseStream.Position + 5 > B.BaseStream.Length)
				return SlowReadRawVarint32();
			int num = B.ReadByte();
			if (num < 128)
				return (uint) num;
			var num2 = num & 127;
			if ((num = B.ReadByte()) < 128)
			{
				num2 |= num << 7;
			}
			else
			{
				num2 |= (num & 127) << 7;
				if ((num = B.ReadByte()) < 128)
				{
					num2 |= num << 14;
				}
				else
				{
					num2 |= (num & 127) << 14;
					if ((num = B.ReadByte()) < 128)
					{
						num2 |= num << 21;
					}
					else
					{
						num2 |= (num & 127) << 21;
						num2 |= (num = B.ReadByte()) << 28;
						if (num >= 128)
							for (var i = 0; i < 5; i++)
								if (ReadRawByte() < 128)
									return (uint) num2;
					}
				}
			}
			return (uint) num2;
		}

		public uint SlowReadRawVarint32()
		{
			int num = ReadRawByte();
			if (num < 128)
				return (uint) num;
			var num2 = num & 127;
			if ((num = ReadRawByte()) < 128)
			{
				num2 |= num << 7;
			}
			else
			{
				num2 |= (num & 127) << 7;
				if ((num = ReadRawByte()) < 128)
				{
					num2 |= num << 14;
				}
				else
				{
					num2 |= (num & 127) << 14;
					if ((num = ReadRawByte()) < 128)
					{
						num2 |= num << 21;
					}
					else
					{
						num2 |= (num & 127) << 21;
						num2 |= (num = ReadRawByte()) << 28;
						if (num >= 128)
							for (var i = 0; i < 5; i++)
								if (ReadRawByte() < 128)
									return (uint) num2;
					}
				}
			}
			return (uint) num2;
		}

		public byte ReadRawByte()
		{
			if (B.BaseStream.Position == B.BaseStream.Length)
				throw new Exception("Erreur de lecture");
			return B.ReadByte();
		}

		public string ReadString()
		{
			var num = (int) ReadRawVarint32();
			if (num == 0)
				return "";
			return Encoding.UTF8.GetString(B.ReadBytes(num));
		}

		public bool ReadBool()
		{
			return ReadRawVarint32() != 0;
		}

		public int DecodeZigZag32(uint n)
		{
			return (int) ((n >> 1) ^ -(int) (n & 1));
		}

		public byte[] ReadMessage(int size)
		{
			return B.ReadBytes(size);
		}

		public ulong ReadRawLittleEndian64()
		{
			ulong num = ReadRawByte();
			ulong num2 = ReadRawByte();
			ulong num3 = ReadRawByte();
			ulong num4 = ReadRawByte();
			ulong num5 = ReadRawByte();
			ulong num6 = ReadRawByte();
			ulong num7 = ReadRawByte();
			ulong num8 = ReadRawByte();

			return num | (num2 << 8) | (num3 << 16) | (num4 << 24) | (num5 << 32) | (num6 << 40) | (num7 << 48) |
			       (num8 << 56);
		}
	}
}