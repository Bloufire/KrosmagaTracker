using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.UIElements
{
    public class Match : INotifyPropertyChanged
    {
        private int _idMatch;
        private string _opponentName;
        private string _playerClasse;
        private int _resultatMatch;
        private int _nbToursMatch;
        private Deck _deck;
        private int _matchType;
        private DateTime date;

        private string _OpponentImage;
        private string _ownImage;

        private bool _extended;

        public Match(SQLiteConnector.Match value)
        {
            IdMatch = value.IdMatch;
            OpponentName = value.OpponentName;
            PlayerClasse = value.PlayerClasse;
            ResultatMatch = value.ResultatMatch;
            NbToursMatch = value.NbToursMatch;
            Deck = new Deck(value.Deck);
            MatchType = value.MatchType;
            Date = value.Date;

            SetImages();
            Extended = false;
        }

        private void SetImages()
        {
            string value = "";
            switch (Deck.OpponentClasse)
            {
                case "Iop":
                    value = "/Images/Gods/Iop.png";
                    break;
                case "Ecaflip":
                    value = "/Images/Gods/Eca.png";
                    break;
                case "Cra":
                    value = "/Images/Gods/Cra.png";
                    break;
                case "Eniripsa":
                    value = "/Images/Gods/Eni.png";
                    break;
                case "Sacrieur":
                    value = "/Images/Gods/Sacri.png";
                    break;
                case "Sadida":
                    value = "/Images/Gods/Sadi.png";
                    break;
                case "Sram":
                    value = "/Images/Gods/Sram.png";
                    break;
                case "Xelor":
                    value = "/Images/Gods/Xel.png";
                    break;
                case "Enutrof":
                    value = "/Images/Gods/Enu.png";
                    break;
            }
            OpponentImage = value;

            switch (PlayerClasse)
            {
                case "Iop":
                    value = "/Images/Gods/Iop.png";
                    break;
                case "Ecaflip":
                    value = "/Images/Gods/Eca.png";
                    break;
                case "Cra":
                    value = "/Images/Gods/Cra.png";
                    break;
                case "Eniripsa":
                    value = "/Images/Gods/Eni.png";
                    break;
                case "Sacrieur":
                    value = "/Images/Gods/Sacri.png";
                    break;
                case "Sadida":
                    value = "/Images/Gods/Sadi.png";
                    break;
                case "Sram":
                    value = "/Images/Gods/Sram.png";
                    break;
                case "Xelor":
                    value = "/Images/Gods/Xel.png";
                    break;
                case "Enutrof":
                    value = "/Images/Gods/Enu.png";
                    break;
            }
            OwnImage = value;
        }

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

        public string OpponentImage
        {
            get
            {
                return _OpponentImage;
            }

            set
            {
                _OpponentImage = value;
            }
        }

        public string OwnImage
        {
            get
            {
                return _ownImage;
            }

            set
            {
                _ownImage = value;
            }
        }

        public bool Extended
        {
            get
            {
                return _extended;
            }

            set
            {
                _extended = value;
                NotifyPropertyChanged("Extended");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
