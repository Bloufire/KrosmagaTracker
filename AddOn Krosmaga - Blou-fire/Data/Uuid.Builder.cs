using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	class Uuid
	{
		private KrosmagaReader reader;

		private bool _hasHighBits;
		private ulong _highBits;
		private bool _hasLowBits;
		private ulong LowBits;

		public bool HasHighBits
		{
			get { return _hasHighBits; }

			set { _hasHighBits = value; }
		}

		public ulong HighBits
		{
			get { return _highBits; }

			set { _highBits = value; }
		}

		public bool HasLowBits
		{
			get { return _hasLowBits; }

			set { _hasLowBits = value; }
		}

		public ulong LowBits1
		{
			get { return LowBits; }

			set { LowBits = value; }
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag != 9)
				{
					if (tag == 17)
					{
						LowBits1 = reader.ReadRawLittleEndian64();
					}
				}
				else
				{
					HighBits = reader.ReadRawLittleEndian64();
				}
			}
		}
	}
}