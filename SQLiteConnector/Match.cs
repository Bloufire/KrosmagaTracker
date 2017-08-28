using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteConnector
{
    public class Match
    {
        private int _idMatch;
        private string _opponentName;
        private string _playerClasse;
        private int _resultatMatch;
        private int _nbToursMatch;
        private Deck _deck;
        private int _matchType;
        private DateTime date;

        public int IdMatch
        {
            get
            {
                return _idMatch;
            }

            set
            {
                _idMatch = value;
            }
        }

        public string OpponentName
        {
            get
            {
                return _opponentName;
            }

            set
            {
                _opponentName = value;
            }
        }

        public string PlayerClasse
        {
            get
            {
                return _playerClasse;
            }

            set
            {
                _playerClasse = value;
            }
        }

        public int ResultatMatch
        {
            get
            {
                return _resultatMatch;
            }

            set
            {
                _resultatMatch = value;
            }
        }

        public int NbToursMatch
        {
            get
            {
                return _nbToursMatch;
            }

            set
            {
                _nbToursMatch = value;
            }
        }

        public Deck Deck
        {
            get
            {
                return _deck;
            }

            set
            {
                _deck = value;
            }
        }

        public int MatchType
        {
            get
            {
                return _matchType;
            }

            set
            {
                _matchType = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }
    }
}
