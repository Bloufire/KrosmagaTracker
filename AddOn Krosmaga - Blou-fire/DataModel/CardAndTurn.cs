using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.UIElements;
using JsonCardsParser;

namespace AddOn_Krosmaga___Blou_fire.DataModel
{
	public class CardAndTurn
	{
		public CardAndTurn()
		{
			
		}
		public CardAndTurn(DeckUI card, int turn)
		{
			DeckUi = card;
			Turn = turn;
		}

		public DeckUI DeckUi { get; set; }
		public int Turn { get; set; }
	}
}
