using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Data;

namespace AddOn_Krosmaga___Blou_fire.Builders.EventsManager
{
    class CardPlayedEvent
    {
        private int _concernedFighter;
        private int _card;
        private int? _relatedCardInstance;
        private int? _relatedTradingCard;
        private int? _triggerer;
        private List<Data.GameEvent> _triggeredEvents;

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

        public CardPlayedEvent(Data.GameEvent value)
        {
            TriggeredEvents = new List<GameEvent>();
            ConcernedFighter = value.Int1;
            Card = value.Int2;
            RelatedTradingCard = value.RelatedTradingCardId;
            RelatedCardInstance = value.RelatedCardInstance;
            Triggerer = value.Triggerer;
            TriggeredEvents = value.TriggeredEvents;
        }
    }
}
