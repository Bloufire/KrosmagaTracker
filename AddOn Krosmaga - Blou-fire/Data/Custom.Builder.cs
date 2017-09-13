using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	class Custom
	{
		private KrosmagaReader reader;

		private bool _hasCustomSlotType;
		private int _customSlotType;
		private bool _hasCustomId;
		private uint _customId;

		public bool HasCustomSlotType
		{
			get { return _hasCustomSlotType; }

			set { _hasCustomSlotType = value; }
		}

		public int CustomSlotType
		{
			get { return _customSlotType; }

			set { _customSlotType = value; }
		}

		public bool HasCustomId
		{
			get { return _hasCustomId; }

			set { _hasCustomId = value; }
		}

		public uint CustomId
		{
			get { return _customId; }

			set { _customId = value; }
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag != 8)
				{
					if (tag == 16)
					{
						CustomId = reader.ReadRawVarint32();
						continue;
					}
				}
				else
				{
					CustomSlotType = (int) reader.ReadRawVarint32();
					continue;
				}
			}
		}
	}
}