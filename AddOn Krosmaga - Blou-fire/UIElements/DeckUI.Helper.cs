using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonCardsParser;

namespace AddOn_Krosmaga___Blou_fire.UIElements
{
	public class DeckUI
	{
		private JsonCardsParser.Card _card;
		private int _cardCount;
        private int _playedTurn;
        private int idObject;

		public DeckUI(Card card, int count)
		{
			Card = card;
			CardCount = count;
		}

		public Card Card
		{
			get { return _card; }

			set { _card = value; }
		}

		public int CardCount
		{
			get { return _cardCount; }

			set { _cardCount = value; }
		}

        public int DrawTurn { get => _playedTurn; set => _playedTurn = value; }
		public int PlayedTurn { get => _playedTurn; set => _playedTurn = value; }
		public int IdObject { get => idObject; set => idObject = value; }
    }
}