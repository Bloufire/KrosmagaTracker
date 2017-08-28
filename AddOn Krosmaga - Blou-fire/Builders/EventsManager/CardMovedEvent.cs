using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Data;
using AddOn_Krosmaga___Blou_fire.Enums;

namespace AddOn_Krosmaga___Blou_fire.Builders.EventsManager
{
    class CardMovedEvent
    {
        private int _concernedFighter;
        private int _card;
        private uint _tradingCard;
        private Enums.CardLocation _cardLocationFrom;
        private Enums.CardLocation _cardLocationTo;
        private int _sequenceIdx;
        private int? _relatedCardInstance;
        private int? _relatedTradingCard;
        private int? _triggerer;
        private List<Data.GameEvent> _triggeredEvents;

        public CardMovedEvent(Data.GameEvent value)
        {
            TriggeredEvents = new List<GameEvent>();
            ConcernedFighter = value.Int1;
            Card = value.Int2;
            CardLocationFrom = (Enums.CardLocation)Enum.Parse(typeof(Enums.CardLocation), value.Int3.ToString());
            CardLocationTo = (Enums.CardLocation)Enum.Parse(typeof(Enums.CardLocation), value.Int4.ToString());
            SequenceIdx = value.Int5;
            TradingCard = value.UInt1;
            RelatedTradingCard = value.RelatedTradingCardId;
            RelatedCardInstance = value.RelatedCardInstance;
            Triggerer = value.Triggerer;
            TriggeredEvents = value.TriggeredEvents;
        }

        public int ConcernedFighter
        {
            get
            {
                return _concernedFighter;
            }

            set
            {
                _concernedFighter = value;
            }
        }

        public int Card
        {
            get
            {
                return _card;
            }

            set
            {
                _card = value;
            }
        }

        public uint TradingCard
        {
            get
            {
                return _tradingCard;
            }

            set
            {
                _tradingCard = value;
            }
        }

        public CardLocation CardLocationFrom
        {
            get
            {
                return _cardLocationFrom;
            }

            set
            {
                _cardLocationFrom = value;
            }
        }

        public CardLocation CardLocationTo
        {
            get
            {
                return _cardLocationTo;
            }

            set
            {
                _cardLocationTo = value;
            }
        }

        public int SequenceIdx
        {
            get
            {
                return _sequenceIdx;
            }

            set
            {
                _sequenceIdx = value;
            }
        }

        internal List<GameEvent> TriggeredEvents
        {
            get
            {
                return _triggeredEvents;
            }

            set
            {
                _triggeredEvents = value;
            }
        }

        public int? RelatedCardInstance
        {
            get
            {
                return _relatedCardInstance;
            }

            set
            {
                _relatedCardInstance = value;
            }
        }

        public int? RelatedTradingCard
        {
            get
            {
                return _relatedTradingCard;
            }

            set
            {
                _relatedTradingCard = value;
            }
        }

        public int? Triggerer
        {
            get
            {
                return _triggerer;
            }

            set
            {
                _triggerer = value;
            }
        }
    }
}
