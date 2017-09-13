using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	class Player
	{
		KrosmagaReader reader;

		private int _index;
		private Data.PlayerProfile _profile;
		private int _godId;
		private string _boardName;

		public Player()
		{
		}

		public int Index
		{
			get { return _index; }

			set { _index = value; }
		}

		internal PlayerProfile Profile
		{
			get { return _profile; }

			set { _profile = value; }
		}

		public int GodId
		{
			get { return _godId; }

			set { _godId = value; }
		}

		public string BoardName
		{
			get { return _boardName; }

			set { _boardName = value; }
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag <= 8)
				{
					if (tag == 8)
					{
						Index = (int) reader.ReadRawVarint32();
						continue;
					}
				}
				else
				{
					if (tag == 18)
					{
						Data.PlayerProfile playerProfile = new Data.PlayerProfile();
						int size = (int) reader.ReadRawVarint32();
						playerProfile.Decode(reader.ReadMessage(size));
						Profile = playerProfile;
						continue;
					}
					if (tag == 24)
					{
						GodId = (int) reader.ReadRawVarint32();
						continue;
					}
					if (tag == 34)
					{
						BoardName = reader.ReadString();
						continue;
					}
				}
			}
		}
	}
}