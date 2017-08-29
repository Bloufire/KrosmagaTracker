using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;
using System.ComponentModel;
using JsonCardsParser;
using AddOn_Krosmaga___Blou_fire.UIElements;
using LiveCharts;
using LiveCharts.Wpf;
using SQLiteConnector;

namespace AddOn_Krosmaga___Blou_fire
{
    
    class UIPlayerDatas : INotifyPropertyChanged
    {
        private bool _hasIndex;
        private int _myIndex;
        private int _currentTurn;
        private int _currentAP;
        private int _maxAP;
        private string _ownPseudo;
        private int _ownVictories;
        private int _ownDefeats;
        private int _ownLevel;
        private string _opponentPseudo;
        private int _opponentVictories;
        private int _opponentDefeats;
        private int _opponentLevel;
        private Enums.GameType _gameType;
        private string _opponentClasse;
        private string _ownClasse;

        private int _ownCardsInHand;
        private int _opponentCardsInHand;

        private List<JsonCardsParser.Card> _opponentPlayedCards;
        private int _opponentFleaux;

        private List<DeckUI> _deck;

        private Queue<int> _actualFleauxIds;

        #region ChartElements
        public SeriesCollection WinrateParClasse { get; set; }
        public SeriesCollection ToursParClasse { get; set; }
        public string[] Labels { get; set; }
        public Separator Separator { get; set; }
        public Func<double, string> Formatter { get; set; }

        private int _winsForThisGroup;
        private int _losesForThisGroup;
        private string _bestMU;
        private string _worstMU;
        private int _nbToursMoy;

        public int WinsForThisGroup
        {
            get { return _winsForThisGroup; }

            set
            {
                _winsForThisGroup = value;
                NotifyPropertyChanged("WinsForThisGroup");
            }
        }

        public int LosesForThisGroup
        {
            get { return _losesForThisGroup; }

            set
            {
                _losesForThisGroup = value;
                NotifyPropertyChanged("LosesForThisGroup");
            }
        }

        public string BestMU
        {
            get { return _bestMU; }

            set
            {
                _bestMU = value;
                NotifyPropertyChanged("BestMU");
            }
        }

        public string WorstMU
        {
            get { return _worstMU; }

            set
            {
                _worstMU = value;
                NotifyPropertyChanged("WorstMU");
            }
        }

        public int NbToursMoy
        {
            get { return _nbToursMoy; }

            set
            {
                _nbToursMoy = value;
                NotifyPropertyChanged("NbToursMoy");
            }
        }

        #endregion

        private List<UIElements.Match> _matchsList;
        private List<UIElements.Match> _matchsWithFilters;

        private string _OwnClasseFilter;
        private string _OpponentClasseFilter;
        private string _OpponentNameFilter;

        public UIPlayerDatas()
        {
            _opponentPlayedCards = new List<JsonCardsParser.Card>();
            ActualFleauxIds = new Queue<int>();
            Deck = new List<DeckUI>();

            MatchsList = new List<UIElements.Match>();
            MatchsWithFilters = new List<UIElements.Match>();

            WinrateParClasse = new SeriesCollection();
            ToursParClasse = new SeriesCollection();

            Labels = new[] { "Iop", "Ecaflip", "Cra", "Eniripsa", "Sacrieur", "Sadida", "Sram", "Xelor", "Enutrof" };
            Separator = new Separator() { Step = 1, IsEnabled = false };
            Formatter = value => value.ToString("N");
        }

        public void AddItemToWinrateParClasse(string title, List<double> values)
        {
            string stringValue = title.Substring(0, 1).ToUpper() + title.Substring(1).ToLower();
            WinrateParClasse.Add(new ColumnSeries
            {
                Title = stringValue,
                Values = new ChartValues<double>(values)
            });
        }

        public void AddItemToToursParClasse(string title, List<double> values)
        {
            string stringValue = title.Substring(0, 1).ToUpper() + title.Substring(1).ToLower();
            ToursParClasse.Add(new ColumnSeries
            {
                Title = stringValue,
                Values = new ChartValues<double>(values)
            });
        }

        public void RemoveItemToWinrateParClasse(string title)
        {
            string stringValue = title.Substring(0, 1).ToUpper() + title.Substring(1).ToLower();
            var value = WinrateParClasse.FirstOrDefault(x => x.Title == stringValue);
            if (value != null)
                WinrateParClasse.Remove(value);
        }

        public void RemoveItemToToursParClasse(string title)
        {
            string stringValue = title.Substring(0, 1).ToUpper() + title.Substring(1).ToLower();
            var value = ToursParClasse.FirstOrDefault(x => x.Title == stringValue);
            if (value != null)
                ToursParClasse.Remove(value);
        }

        public void AddCardToDeck(DeckUI card)
        {
            _deck.Add(card);
        }

        public void ClearDeck()
        {
            _deck.Clear();
        }

        public int CurrentTurn
        {
            get { return _currentTurn; }

            set
            {
                _currentTurn = value;
                NotifyPropertyChanged("CurrentTurn");
            }
        }

        public int CurrentAP
        {
            get { return _currentAP; }

            set
            {
                _currentAP = value;
                NotifyPropertyChanged("CurrentAP");

            }
        }

        public int MaxAP
        {
            get { return _maxAP; }

            set
            {
                _maxAP = value;
                NotifyPropertyChanged("MaxAP");

            }
        }

        public string OpponentPseudo
        {
            get { return _opponentPseudo ?? "Pseudo == Null"; }

            set
            {
                _opponentPseudo = value;
                NotifyPropertyChanged("OpponentPseudo");

            }
        }

        public int OpponentVictories
        {
            get { return _opponentVictories; }

            set
            {
                _opponentVictories = value;
                NotifyPropertyChanged("OpponentVictories");

            }
        }

        public int OpponentDefeats
        {
            get { return _opponentDefeats; }

            set
            {
                _opponentDefeats = value;
                NotifyPropertyChanged("OpponentDefeats");

            }
        }

        public string OwnPseudo
        {
            get { return _ownPseudo ?? "OwnPseudo == null"; }

            set
            {
                _ownPseudo = value;
                NotifyPropertyChanged("OwnPseudo");

            }
        }

        public int OwnVictories
        {
            get { return _ownVictories; }

            set
            {
                _ownVictories = value;
                NotifyPropertyChanged("OwnVictories");

            }
        }

        public int OwnDefeats
        {
            get { return _ownDefeats; }

            set
            {
                _ownDefeats = value;
                NotifyPropertyChanged("OwnDefeats");

            }
        }

        public int OwnLevel
        {
            get { return _ownLevel; }

            set
            {
                _ownLevel = value;
                NotifyPropertyChanged("OwnLevel");

            }
        }

        public int OpponentLevel
        {
            get { return _opponentLevel; }

            set
            {
                _opponentLevel = value;
                NotifyPropertyChanged("OpponentLevel");
            }
        }

        public int MyIndex
        {
            get { return _myIndex; }

            set
            {
                _myIndex = value;
                NotifyPropertyChanged("MyIndex");

            }
        }

        public GameType GameType
        {
            get { return _gameType; }

            set
            {
                _gameType = value;
                NotifyPropertyChanged("GameType");
            }
        }

        public bool HasIndex
        {
            get { return _hasIndex; }

            set
            {
                _hasIndex = value;
                NotifyPropertyChanged("HasIndex");
            }
        }

        public List<JsonCardsParser.Card> OpponentPlayedCards
        {
            get { return _opponentPlayedCards; }

            set
            {
                _opponentPlayedCards = value;
                NotifyPropertyChanged("OpponentPlayedCards");
            }
        }

        public int OpponentFleaux
        {
            get { return _opponentFleaux; }

            set
            {
                _opponentFleaux = value;
                NotifyPropertyChanged("OpponentFleaux");
            }
        }

        public Queue<int> ActualFleauxIds
        {
            get { return _actualFleauxIds; }

            set
            {
                _actualFleauxIds = value;
                NotifyPropertyChanged("ActualFleauxIds");
            }
        }

        public List<DeckUI> Deck
        {
            get
            {
                return _deck.OrderBy(x => x.Card.CostAP).ToList();
            }

            set
            {
                _deck = value;
                NotifyPropertyChanged("Deck");
            }
        }

        public string OwnClasse
        {
            get { return _ownClasse ?? "OwnClasse == Null"; }

            set { _ownClasse = value; }
        }

        public string OpponentClasse
        {
            get { return _opponentClasse ?? "OpponentClasse == Null"; }

            set { _opponentClasse = value; }
        }

        public List<UIElements.Match> MatchsList
        {
            get { return _matchsList; }

            set
            {
                _matchsList = value;
                NotifyPropertyChanged("MatchsList");
            }
        }

        public int OwnCardsInHand
        {
            get { return _ownCardsInHand; }

            set
            {
                _ownCardsInHand = value;
                NotifyPropertyChanged("OwnCardsInHand");
            }
        }

        public int OpponentCardsInHand
        {
            get { return _opponentCardsInHand; }

            set
            {
                _opponentCardsInHand = value;
                NotifyPropertyChanged("OpponentCardsInHand");
            }
        }

        public List<UIElements.Match> MatchsWithFilters
        {
            get
            {
                return _matchsWithFilters.OrderByDescending(x => x.Date).ToList();
            }

            set
            {
                _matchsWithFilters = value;
                NotifyPropertyChanged("MatchsWithFilters");
            }
        }

        public string OwnClasseFilter
        {
            get { return _OwnClasseFilter ?? "OwnClasseFilter == Null"; }

            set
            {
                _OwnClasseFilter = value;
                UpdateMatchsWithFilterList();
                NotifyPropertyChanged("OwnClasseFilter");
            }
        }

        public string OpponentClasseFilter
        {
            get { return _OpponentClasseFilter ?? "OwnClasseFilter == Null"; }

            set
            {
                _OpponentClasseFilter = value;
                UpdateMatchsWithFilterList();
                NotifyPropertyChanged("OpponentClasseFilter");
            }
        }

        public string OpponentNameFilter
        {
            get { return _OpponentNameFilter; }

            set
            {
                _OpponentNameFilter = value;
                UpdateMatchsWithFilterList();
                NotifyPropertyChanged("OpponentNameFilter");
            }
        }

        private void UpdateMatchsWithFilterList()
        {
            MatchsWithFilters.Clear();
            MatchsWithFilters = MatchsList.Where(x => 
            (OwnClasseFilter == "Tous" || OwnClasseFilter == null ? true : x.PlayerClasse == OwnClasseFilter) && 
            (OpponentClasseFilter == "Tous" || OpponentClasseFilter == null ? true : x.Deck.OpponentClasse == OpponentClasseFilter) &&
            (String.IsNullOrEmpty(OpponentNameFilter) ? true : x.OpponentName.ToLower().Contains(OpponentNameFilter.ToLower()))).ToList();
            NotifyPropertyChanged("MatchsWithFilters");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
