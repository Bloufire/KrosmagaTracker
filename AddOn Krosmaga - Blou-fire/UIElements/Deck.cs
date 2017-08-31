using JsonCardsParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.UIElements
{
    public class Deck
    {
        private int _idDeck;
        private string _opponentClasse;
        private List<DeckUI> _cardsList;

        public Deck(SQLiteConnector.Deck value)
        {
            CardsList = new List<DeckUI>();

            IdDeck = value.IdDeck;
            OpponentClasse = value.OpponentClasse;
        }

        public int IdDeck
        {
            get
            {
                return _idDeck;
            }

            set
            {
                _idDeck = value;
            }
        }

        public string OpponentClasse
        {
            get
            {
                return _opponentClasse;
            }

            set
            {
                _opponentClasse = value;
            }
        }

        public List<DeckUI> CardsList
        {
            get
            {
                return _cardsList;
            }

            set
            {
                _cardsList = value;
            }
        }
    }
}
