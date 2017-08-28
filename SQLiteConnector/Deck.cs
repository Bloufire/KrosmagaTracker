using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteConnector
{
    public class Deck
    {
        private int _idDeck;
        private string _opponentClasse;
        private List<Card> _cardsList;
        
        public Deck()
        {
            CardsList = new List<Card>();
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

        public List<Card> CardsList
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
