using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	class ClientPreferences
	{
		private KrosmagaReader reader;

		private bool _hasLastSelectedDeckId;
		private Data.Uuid _lastSelectedDeckId;
		private bool _hasLastDungeonDeckId;
		private Data.Uuid _lastDungeonDeckId;
		private bool _hasLastGameType;
		private Enums.GameType LastGameType;
		private List<Data.CustomByGod> _customByGodList;
		private int _customsByGodCount;
		private List<Data.SelectedDeckByGod> _selectedDecksByGodId;
		private int _selectedDecksByGodIdCount;

		public ClientPreferences()
		{
			CustomByGodList = new List<CustomByGod>();
			SelectedDecksByGodId = new List<SelectedDeckByGod>();
		}

		public bool HasLastSelectedDeckId
		{
			get { return _hasLastSelectedDeckId; }

			set { _hasLastSelectedDeckId = value; }
		}

		internal Uuid LastSelectedDeckId
		{
			get { return _lastSelectedDeckId; }

			set { _lastSelectedDeckId = value; }
		}

		public bool HasLastDungeonDeckId
		{
			get { return _hasLastDungeonDeckId; }

			set { _hasLastDungeonDeckId = value; }
		}

		public bool HasLastGameType
		{
			get { return _hasLastGameType; }

			set { _hasLastGameType = value; }
		}

		internal Uuid LastDungeonDeckId
		{
			get { return _lastDungeonDeckId; }

			set { _lastDungeonDeckId = value; }
		}

		public GameType LastGameType1
		{
			get { return LastGameType; }

			set { LastGameType = value; }
		}

		internal List<CustomByGod> CustomByGodList
		{
			get { return _customByGodList; }

			set { _customByGodList = value; }
		}

		public int CustomsByGodCount
		{
			get { return _customsByGodCount; }

			set { _customsByGodCount = value; }
		}

		internal List<SelectedDeckByGod> SelectedDecksByGodId
		{
			get { return _selectedDecksByGodId; }

			set { _selectedDecksByGodId = value; }
		}

		public int SelectedDecksByGodIdCount
		{
			get { return _selectedDecksByGodIdCount; }

			set { _selectedDecksByGodIdCount = value; }
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag <= 26)
				{
					if (tag == 10)
					{
						Data.Uuid value = new Data.Uuid();
						int size = (int) reader.ReadRawVarint32();
						value.Decode(reader.ReadMessage(size));
						LastSelectedDeckId = value;
						continue;
					}
					if (tag == 26)
					{
						Data.Uuid value = new Data.Uuid();
						int size = (int) reader.ReadRawVarint32();
						value.Decode(reader.ReadMessage(size));
						LastDungeonDeckId = value;
						continue;
					}
				}
				else if (tag != 32)
				{
					if (tag == 42)
					{
						Data.CustomByGod value = new Data.CustomByGod();
						int size = (int) reader.ReadRawVarint32();
						value.Decode(reader.ReadMessage(size));
						CustomByGodList.Add(value);
						continue;
					}
					if (tag == 50)
					{
						Data.SelectedDeckByGod value = new Data.SelectedDeckByGod();
						int size = (int) reader.ReadRawVarint32();
						value.Decode(reader.ReadMessage(size));
						SelectedDecksByGodId.Add(value);
						continue;
					}
				}
				else
				{
					LastGameType1 = (Enums.GameType) Enum.Parse(typeof(Enums.GameType), reader.ReadRawVarint32().ToString());
					continue;
				}
			}
		}
	}
}