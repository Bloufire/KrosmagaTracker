using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Data;
using AddOn_Krosmaga___Blou_fire.Enums;
using System.IO;

namespace AddOn_Krosmaga___Blou_fire.Builders
{
	class GameStarted
	{
		private KrosmagaReader reader;

		private List<Data.Player> _playersList;
		private int _playersCount;
		private string _backgroundName;
		private int _boardColumnsCount;
		private int _boardRowsCount;
		private int _myIndex;
		private int _myCardsCount;
		private int _opponentCardsCount;
		private Enums.GameType _gameType;
		private int _dofusBaseLife;
		private int _draftAllInOwnAmount;
		private int _draftAllInOpponentAmount;

		#region getter / setter

		internal List<Player> PlayersList
		{
			get { return _playersList; }

			set { _playersList = value; }
		}

		public int PlayersCount
		{
			get { return _playersCount; }

			set { _playersCount = value; }
		}

		public string BackgroundName
		{
			get { return _backgroundName; }

			set { _backgroundName = value; }
		}

		public int BoardColumnsCount
		{
			get { return _boardColumnsCount; }

			set { _boardColumnsCount = value; }
		}

		public int BoardRowsCount
		{
			get { return _boardRowsCount; }

			set { _boardRowsCount = value; }
		}

		public int MyIndex
		{
			get { return _myIndex; }

			set { _myIndex = value; }
		}

		public int MyCardsCount
		{
			get { return _myCardsCount; }

			set { _myCardsCount = value; }
		}

		public int OpponentCardsCount
		{
			get { return _opponentCardsCount; }

			set { _opponentCardsCount = value; }
		}

		public GameType GameType
		{
			get { return _gameType; }

			set { _gameType = value; }
		}

		public int DofusBaseLife
		{
			get { return _dofusBaseLife; }

			set { _dofusBaseLife = value; }
		}

		public int DraftAllInOwnAmount
		{
			get { return _draftAllInOwnAmount; }

			set { _draftAllInOwnAmount = value; }
		}

		public int DraftAllInOpponentAmount
		{
			get { return _draftAllInOpponentAmount; }

			set { _draftAllInOpponentAmount = value; }
		}

		#endregion

		public GameStarted()
		{
			PlayersList = new List<Player>();
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag <= 48)
				{
					if (tag <= 18)
					{
						if (tag == 10)
						{
							Data.Player player = new Data.Player();
							int size = (int) reader.ReadRawVarint32();
							player.Decode(reader.ReadMessage(size));
							PlayersList.Add(player);
							continue;
						}
						if (tag == 18)
						{
							BackgroundName = reader.ReadString();
							continue;
						}
					}
					else
					{
						if (tag == 24)
						{
							BoardColumnsCount = (int) reader.ReadRawVarint32();
							continue;
						}
						if (tag == 32)
						{
							BoardRowsCount = (int) reader.ReadRawVarint32();
							continue;
						}
						if (tag == 48)
						{
							MyIndex = (int) reader.ReadRawVarint32();
							continue;
						}
					}
				}
				else if (tag <= 72)
				{
					if (tag == 56)
					{
						MyCardsCount = (int) reader.ReadRawVarint32();
						continue;
					}
					if (tag == 64)
					{
						OpponentCardsCount = (int) reader.ReadRawVarint32();
						continue;
					}
					if (tag == 72)
					{
						GameType = (Enums.GameType) Enum.Parse(typeof(Enums.GameType), reader.ReadRawVarint32().ToString());
						continue;
					}
				}
				else
				{
					if (tag == 80)
					{
						DofusBaseLife = (int) reader.ReadRawVarint32();
						continue;
					}
					if (tag == 88)
					{
						DraftAllInOwnAmount = (int) reader.ReadRawVarint32();
						continue;
					}
					if (tag == 96)
					{
						DraftAllInOpponentAmount = (int) reader.ReadRawVarint32();
						continue;
					}
				}
			}
		}
	}
}