using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	class DeckInfo
	{
		private KrosmagaReader reader;

		private bool _hasUuid;
		private Data.Uuid Uuid;
		private bool _hasGodId;
		private int _godId;
		private bool _hasName;
		private string _name;
		private List<uint> _cardsWithQty1List;
		private int _cardsWithQty1Count;
		private List<uint> _cardsWithQty2List;
		private int _cardsWithQty2Count;
		private List<uint> _cardsWithQty3List;
		private int _cardsWithQty3Count;

		public DeckInfo()
		{
			CardsWithQty1List = new List<uint>();
			CardsWithQty2List = new List<uint>();
			CardsWithQty3List = new List<uint>();
		}

		public bool HasUuid
		{
			get { return _hasUuid; }

			set { _hasUuid = value; }
		}

		internal Uuid Uuid1
		{
			get { return Uuid; }

			set { Uuid = value; }
		}

		public bool HasGodId
		{
			get { return _hasGodId; }

			set { _hasGodId = value; }
		}

		public int GodId
		{
			get { return _godId; }

			set { _godId = value; }
		}

		public bool HasName
		{
			get { return _hasName; }

			set { _hasName = value; }
		}

		public string Name
		{
			get { return _name; }

			set { _name = value; }
		}

		public List<uint> CardsWithQty1List
		{
			get { return _cardsWithQty1List; }

			set { _cardsWithQty1List = value; }
		}

		public int CardsWithQty1Count
		{
			get { return _cardsWithQty1Count; }

			set { _cardsWithQty1Count = value; }
		}

		public List<uint> CardsWithQty2List
		{
			get { return _cardsWithQty2List; }

			set { _cardsWithQty2List = value; }
		}

		public int CardsWithQty2Count
		{
			get { return _cardsWithQty2Count; }

			set { _cardsWithQty2Count = value; }
		}

		public List<uint> CardsWithQty3List
		{
			get { return _cardsWithQty3List; }

			set { _cardsWithQty3List = value; }
		}

		public int CardsWithQty3Count
		{
			get { return _cardsWithQty3Count; }

			set { _cardsWithQty3Count = value; }
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag <= 16)
				{
					if (tag == 10)
					{
						Data.Uuid value = new Data.Uuid();
						int size = (int) reader.ReadRawVarint32();
						value.Decode(reader.ReadMessage(size));
						Uuid1 = value;
						continue;
					}
					if (tag == 16)
					{
						GodId = (int) reader.ReadRawVarint32();
						continue;
					}
				}
				else if (tag <= 34)
				{
					if (tag == 26)
					{
						Name = reader.ReadString();
						continue;
					}
					switch (tag)
					{
						case 32:
						case 34:
							int size = (int) reader.ReadRawVarint32();
							long sizeInBytes = reader.B.BaseStream.Position + size;
							while (reader.B.BaseStream.Position < sizeInBytes)
							{
								CardsWithQty1List.Add(reader.ReadRawVarint32());
							}
							continue;
					}
				}
				else
				{
					switch (tag)
					{
						case 40:
						case 42:
							int size = (int) reader.ReadRawVarint32();
							long sizeInBytes = reader.B.BaseStream.Position + size;
							while (reader.B.BaseStream.Position < sizeInBytes)
							{
								CardsWithQty2List.Add(reader.ReadRawVarint32());
							}
							continue;
						case 41:
							break;
						default:
							switch (tag)
							{
								case 48:
								case 50:
									int size2 = (int) reader.ReadRawVarint32();
									long sizeInBytes2 = reader.B.BaseStream.Position + size2;
									while (reader.B.BaseStream.Position < sizeInBytes2)
									{
										CardsWithQty3List.Add(reader.ReadRawVarint32());
									}
									continue;
							}
							break;
					}
				}
			}
		}
	}
}