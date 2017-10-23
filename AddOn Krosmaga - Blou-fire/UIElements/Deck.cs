using JsonCardsParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.UIElements
{
	public class Deck : ObservableObject
	{
		private int _idDeck;
		private string _opponentClasse;
		private List<DeckUI> _cardsList;
		private SQLiteConnector.Deck sqlDeck;
		public Deck(SQLiteConnector.Deck value)
		{
			CardsList = new List<DeckUI>();
			sqlDeck = value;
			IdDeck = value.IdDeck;
			OpponentClasse = value.OpponentClasse;
		}

		public int IdDeck
		{
			get { return _idDeck; }

			set { _idDeck = value; }
		}

		public string OpponentClasse
		{
			get { return _opponentClasse; }

			set { _opponentClasse = value; }
		}

		public List<DeckUI> CardsList
		{
			get { return _cardsList; }

			set
			{
				_cardsList = value;
				OnPropertyChanged("CardsList");
			}
		}

		public void UpdateCardList()
		{
			CardsList = Helpers.Helpers.TransformCardListToDeckUiList(sqlDeck.CardsList);
		}
	}
}