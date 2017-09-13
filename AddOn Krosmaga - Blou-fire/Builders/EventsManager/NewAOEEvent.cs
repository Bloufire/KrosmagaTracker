using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Data;

namespace AddOn_Krosmaga___Blou_fire.Builders.EventsManager
{
	class NewAOEEvent
	{
		private int _concernedFightObject;
		private int _ownerFighter;
		private Data.CellCoord _where;
		private uint _tradingCardId;
		private int _cardInstanceId;
		private int? _relatedCardInstance;
		private int? _relatedTradingCard;
		private int? _triggerer;
		private List<Data.GameEvent> _triggeredEvents;

		public NewAOEEvent(Data.GameEvent value)
		{
			TriggeredEvents = new List<GameEvent>();

			ConcernedFightObject = value.Int1;
			OwnerFighter = value.Int2;
			CardInstanceId = value.Int3;
			Where = value.CellCoord1;
			TradingCardId = value.UInt1;
			RelatedTradingCard = value.RelatedTradingCardId;
			RelatedCardInstance = value.RelatedCardInstance;
			Triggerer = value.Triggerer;
			TriggeredEvents = value.TriggeredEvents;
		}

		public int ConcernedFightObject
		{
			get { return _concernedFightObject; }

			set { _concernedFightObject = value; }
		}

		public int OwnerFighter
		{
			get { return _ownerFighter; }

			set { _ownerFighter = value; }
		}

		internal CellCoord Where
		{
			get { return _where; }

			set { _where = value; }
		}

		public uint TradingCardId
		{
			get { return _tradingCardId; }

			set { _tradingCardId = value; }
		}

		public int CardInstanceId
		{
			get { return _cardInstanceId; }

			set { _cardInstanceId = value; }
		}

		public int? RelatedCardInstance
		{
			get { return _relatedCardInstance; }

			set { _relatedCardInstance = value; }
		}

		public int? RelatedTradingCard
		{
			get { return _relatedTradingCard; }

			set { _relatedTradingCard = value; }
		}

		public int? Triggerer
		{
			get { return _triggerer; }

			set { _triggerer = value; }
		}

		internal List<GameEvent> TriggeredEvents
		{
			get { return _triggeredEvents; }

			set { _triggeredEvents = value; }
		}
	}
}