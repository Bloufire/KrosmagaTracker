using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<int> _playedTurn;
		private int _drawTurn;

		private int idObject;

		public DeckUI(Card card, int count)
		{
			Card = card;
			CardCount = count;
			_playedTurn = new ObservableCollection<int>();
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

		public override string ToString()
		{
			return Card.Name + "  x" + CardCount;
		}

		public int DrawTurn { get => _drawTurn; set => _drawTurn = value; }
		public ObservableCollection<int> PlayedTurn { get => _playedTurn; set => _playedTurn = value; }
		public int IdObject { get => idObject; set => idObject = value; }
    }
}