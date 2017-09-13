using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Data;

namespace AddOn_Krosmaga___Blou_fire.Builders
{
	class PlayerAccountLoaded
	{
		private KrosmagaReader reader;

		private bool _hasNickname;
		private string _nickname;
		private bool _hasSeasonRank;
		private Data.PlayerRankInfo _seasonRank;
		private bool _hasTutorialStep;
		private int _tutorialStep;
		private bool _hasKamas;
		private int _kamas;
		private bool _hasSelectedCardBack;
		private uint _selectedCardBack;
		private List<int> _godDecksUnlockedList;
		private int _godDeckUnlockedCount;
		private bool _hasClientPrefs;
		private Data.ClientPreferences _clientPrefs;
		private bool _hasFragments;
		private int _fragments;
		private bool _hasWheelToken;
		private int _wheelToken;
		private List<int> _tutorialBlocksList;
		private int _tutorialBlocksCount;
		private bool _hasAdmin;
		private bool _admin;
		private List<Data.DeckInfo> _decksList;
		private int _decksCount;
		private bool _hasAccountType;
		private string _accountType;
		private List<int> _seasonsToRewardList;
		private int _seasonsToRewardCount;
		private bool _hasHash;
		private int _hash;
		private bool _hasFirstDraft;
		private bool _firstDraft;

		public PlayerAccountLoaded()
		{
			GodDecksUnlockedList = new List<int>();
			TutorialBlocksList = new List<int>();
			DecksList = new List<DeckInfo>();
			SeasonsToRewardList = new List<int>();
		}

		public bool HasNickname
		{
			get { return _hasNickname; }

			set { _hasNickname = value; }
		}

		public string Nickname
		{
			get { return _nickname; }

			set { _nickname = value; }
		}

		public bool HasSeasonRank
		{
			get { return _hasSeasonRank; }

			set { _hasSeasonRank = value; }
		}

		internal PlayerRankInfo SeasonRank
		{
			get { return _seasonRank; }

			set { _seasonRank = value; }
		}

		public bool HasTutorialStep
		{
			get { return _hasTutorialStep; }

			set { _hasTutorialStep = value; }
		}

		public int TutorialStep
		{
			get { return _tutorialStep; }

			set { _tutorialStep = value; }
		}

		public bool HasKamas
		{
			get { return _hasKamas; }

			set { _hasKamas = value; }
		}

		public int Kamas
		{
			get { return _kamas; }

			set { _kamas = value; }
		}

		public bool HasSelectedCardBack
		{
			get { return _hasSelectedCardBack; }

			set { _hasSelectedCardBack = value; }
		}

		public uint SelectedCardBack
		{
			get { return _selectedCardBack; }

			set { _selectedCardBack = value; }
		}

		public List<int> GodDecksUnlockedList
		{
			get { return _godDecksUnlockedList; }

			set { _godDecksUnlockedList = value; }
		}

		public int GodDeckUnlockedCount
		{
			get { return _godDeckUnlockedCount; }

			set { _godDeckUnlockedCount = value; }
		}

		public bool HasClientPrefs
		{
			get { return _hasClientPrefs; }

			set { _hasClientPrefs = value; }
		}

		internal ClientPreferences ClientPrefs
		{
			get { return _clientPrefs; }

			set { _clientPrefs = value; }
		}

		public bool HasFragments
		{
			get { return _hasFragments; }

			set { _hasFragments = value; }
		}

		public int Fragments
		{
			get { return _fragments; }

			set { _fragments = value; }
		}

		public bool HasWheelToken
		{
			get { return _hasWheelToken; }

			set { _hasWheelToken = value; }
		}

		public int WheelToken
		{
			get { return _wheelToken; }

			set { _wheelToken = value; }
		}

		public List<int> TutorialBlocksList
		{
			get { return _tutorialBlocksList; }

			set { _tutorialBlocksList = value; }
		}

		public int TutorialBlocksCount
		{
			get { return _tutorialBlocksCount; }

			set { _tutorialBlocksCount = value; }
		}

		public bool HasAdmin
		{
			get { return _hasAdmin; }

			set { _hasAdmin = value; }
		}

		public bool Admin
		{
			get { return _admin; }

			set { _admin = value; }
		}

		internal List<DeckInfo> DecksList
		{
			get { return _decksList; }

			set { _decksList = value; }
		}

		public int DecksCount
		{
			get { return _decksCount; }

			set { _decksCount = value; }
		}

		public bool HasAccountType
		{
			get { return _hasAccountType; }

			set { _hasAccountType = value; }
		}

		public string AccountType
		{
			get { return _accountType; }

			set { _accountType = value; }
		}

		public List<int> SeasonsToRewardList
		{
			get { return _seasonsToRewardList; }

			set { _seasonsToRewardList = value; }
		}

		public int SeasonsToRewardCount
		{
			get { return _seasonsToRewardCount; }

			set { _seasonsToRewardCount = value; }
		}

		public bool HasHash
		{
			get { return _hasHash; }

			set { _hasHash = value; }
		}

		public int Hash
		{
			get { return _hash; }

			set { _hash = value; }
		}

		public bool HasFirstDraft
		{
			get { return _hasFirstDraft; }

			set { _hasFirstDraft = value; }
		}

		public bool FirstDraft
		{
			get { return _firstDraft; }

			set { _firstDraft = value; }
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag <= 58)
				{
					if (tag <= 24)
					{
						if (tag == 10)
						{
							Nickname = reader.ReadString();
							continue;
						}
						if (tag == 18)
						{
							Data.PlayerRankInfo playerRankInfo = new Data.PlayerRankInfo();
							int size = (int) reader.ReadRawVarint32();
							playerRankInfo.Decode(reader.ReadMessage(size));
							SeasonRank = playerRankInfo;
							continue;
						}
						if (tag == 24)
						{
							TutorialStep = (int) reader.ReadRawVarint32();
							continue;
						}
					}
					else if (tag <= 40)
					{
						if (tag == 32)
						{
							Kamas = (int) reader.ReadRawVarint32();
							continue;
						}
						if (tag == 40)
						{
							SelectedCardBack = reader.ReadRawVarint32();
							continue;
						}
					}
					else
					{
						switch (tag)
						{
							case 48:
							case 50:
								int size = (int) reader.ReadRawVarint32();
								long sizeInBytes = reader.B.BaseStream.Position + size;
								while (reader.B.BaseStream.Position < sizeInBytes)
								{
									GodDecksUnlockedList.Add((int) reader.ReadRawVarint32());
								}
								continue;
							case 49:
								break;
							default:
								if (tag == 58)
								{
									Data.ClientPreferences value = new Data.ClientPreferences();
									int size2 = (int) reader.ReadRawVarint32();
									value.Decode(reader.ReadMessage(size2));
									ClientPrefs = value;
									continue;
								}
								break;
						}
					}
				}
				else if (tag <= 88)
				{
					if (tag <= 72)
					{
						if (tag == 64)
						{
							Fragments = (int) reader.ReadRawVarint32();
							continue;
						}
						if (tag == 72)
						{
							WheelToken = (int) reader.ReadRawVarint32();
							continue;
						}
					}
					else
					{
						switch (tag)
						{
							case 80:
							case 82:
								int size = (int) reader.ReadRawVarint32();
								long sizeInBytes = reader.B.BaseStream.Position + size;
								while (reader.B.BaseStream.Position < sizeInBytes)
								{
									TutorialBlocksList.Add((int) reader.ReadRawVarint32());
								}
								continue;
							case 81:
								break;
							default:
								if (tag == 88)
								{
									Admin = reader.ReadBool();
									continue;
								}
								break;
						}
					}
				}
				else if (tag <= 106)
				{
					if (tag == 98)
					{
						Data.DeckInfo value = new Data.DeckInfo();
						int size = (int) reader.ReadRawVarint32();
						value.Decode(reader.ReadMessage(size));
						DecksList.Add(value);
						continue;
					}
					if (tag == 106)
					{
						AccountType = reader.ReadString();
						continue;
					}
				}
				else
				{
					switch (tag)
					{
						case 112:
						case 114:
							int size = (int) reader.ReadRawVarint32();
							long sizeInBytes = reader.B.BaseStream.Position + size;
							while (reader.B.BaseStream.Position < sizeInBytes)
							{
								SeasonsToRewardList.Add((int) reader.ReadRawVarint32());
							}
							continue;
						case 113:
							break;
						default:
							if (tag == 120)
							{
								Hash = (int) reader.ReadRawVarint32();
								continue;
							}
							if (tag == 128)
							{
								FirstDraft = reader.ReadBool();
								continue;
							}
							break;
					}
				}
			}
		}
	}
}