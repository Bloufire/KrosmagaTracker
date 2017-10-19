using AddOn_Krosmaga___Blou_fire.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.UIElements;
using JsonCardsParser;

namespace AddOn_Krosmaga___Blou_fire.Models
{
	/// <summary>
	/// Model contenant toutes les informations que le tracker/lecture en base peuvent remonter. Et qui seront utilisable pour les vues.
	/// </summary>
	public class TrackerModel : ObservableObject
	{
		public TrackerModel()
		{
			
		}

		#region Own Profile Data

		#region OwnPseudo

		private string _ownPseudo;

		public string OwnPseudo
		{
			get => _ownPseudo;

			set
			{
				if (_ownPseudo == value) _ownPseudo = "default first";
				_ownPseudo = value;
				OnPropertyChanged("OwnPseudo");
			}
		}

		#endregion

		#region OwnWinsNb

		private int _ownWinsNb;

		public int OwnWinsNb
		{
			get => _ownWinsNb;

			set
			{
				if (_ownWinsNb == null)
				{
					_ownWinsNb = -1;
				}

				_ownWinsNb = value;
				OnPropertyChanged("OwnWinsNb");
			}
		}

		#endregion

		#region OwnLosesNb

		private int _ownLosesNb;

		public int OwnLosesNb
		{
			get => _ownLosesNb;

			set
			{
				if (_ownLosesNb == null)
				{
					_ownLosesNb = -1;
				}

				_ownLosesNb = value;
				OnPropertyChanged("OwnLosesNb");
			}
		}

		#endregion

		#region OwnLevel

		private int _ownLevel;

		public int OwnLevel
		{
			get { return _ownLevel; }
			set
			{
				_ownLevel = value;
				OnPropertyChanged("OwnLevel");
			}
		}

		#endregion

		#region OwnClasse

		private string _ownClasse;

		public string OwnClasse
		{
			get { return _ownClasse; }
			set
			{
				_ownClasse = value;
				OnPropertyChanged("OwnClasse");
			}
		}

		#endregion

		#endregion

		#region Vs Profile Data

		#region VsPseudo

		private string _vsPseudo;

		public string VsPseudo
		{
			get { return _vsPseudo; }
			set
			{
				_vsPseudo = value;
				OnPropertyChanged("VsPseudo");
			}
		}

		#endregion


		#region VsWinsNb

		private int _vsWinsNb;

		public int VsWinsNb
		{
			get { return _vsWinsNb; }
			set
			{
				_vsWinsNb = value;
				OnPropertyChanged("VsWinsNb");
			}
		}

		#endregion

		#region VsLosesNb

		private int _vsLosesNb;

		public int VsLosesNb
		{
			get { return _vsLosesNb; }
			set
			{
				_vsLosesNb = value;
				OnPropertyChanged("VsLosesNb");
			}
		}

		#endregion

		#region OpponentLevel

		private int _opponentLevel;

		public int OpponentLevel
		{
			get { return _opponentLevel; }
			set
			{
				_opponentLevel = value;
				OnPropertyChanged("OpponentLevel");
			}
		}

		#endregion

		#region OpponentClasse

		private string _opponentClasse;

		public string OpponentClasse
		{
			get { return _opponentClasse; }
			set
			{
				_opponentClasse = value;
				OnPropertyChanged("OpponentClasse");
			}
		}

		#endregion

		#endregion


		#region HistoMatches

		private List<Match> _filteredGames;


		public List<Match> FilteredGames
		{
			get
			{
				if (_filteredGames != null) return _filteredGames;
				return new List<Match>();
			}
			set
			{
				_filteredGames = value;
				OnPropertyChanged("FilteredGames");
			}
		}

		private GameType _gameType;

		public GameType GameType
		{
			get { return _gameType; }
			set
			{
				_gameType = value;
				OnPropertyChanged("GameType");
			}
		}


		private int _myIndex;

		/// <summary>
		/// MyIndex c'est un chiffre qui indique quel joueur tu es
		/// Joueur 0 : commence en premier
		///	Joueur 1 : commence en 2
		/// </summary>
		public int MyIndex
		{
			get { return _myIndex; }
			set
			{
				_myIndex = value;
				OnPropertyChanged("MyIndex");
			}
		}

		#endregion

		#region CardsInHands

		private Queue<int> _actualFleauxIds;

		private int _ownCardsInHand;
		private int _opponentCardsInHand;
		private int _currentTurn;
		private int _nbFleau;
		private List<DeckUI> _deck;

		private List<Card> _opponentPlayedCards;
		private List<DeckUI> _cardAlreadyPlayed;
		private List<DeckUI> _cardsInHand;
		private List<DeckUI> _deckInfinites;
		private List<DeckUI> _deckKrosmiques;

        private List<KeyValuePair<int, int>> _cardIdsByTurn;


        public Queue<int> ActualFleauxIds
		{
			get
            {
                if (_actualFleauxIds != null) return _actualFleauxIds;
                _actualFleauxIds = new Queue<int>();
                return _actualFleauxIds;
            }

			set
			{
				_actualFleauxIds = value;
				OnPropertyChanged("ActualFleauxIds");
			}
		}
		public int OwnCardsInHand
		{
			get { return _ownCardsInHand; }
			set
			{
				_ownCardsInHand = value;
				OnPropertyChanged("OwnCardsInHand");
			}
		}

		public int OpponentCardsInHand
		{
			get { return _opponentCardsInHand; }
			set
			{
				_opponentCardsInHand = value;
				OnPropertyChanged("OpponentCardsInHand");
			}
		}

        public List<KeyValuePair<int, int>> CardIdsByTurn
        {
            get
            {
                if (_cardIdsByTurn != null) return _cardIdsByTurn;
                _cardIdsByTurn = new List<KeyValuePair<int,int>>();
                return _cardIdsByTurn;
            }

            set
            {
                _cardIdsByTurn = value;
                OnPropertyChanged("CardIdsByTurn");
            }
        }

		public int CurrentTurn
		{
			get { return _currentTurn; }
			set
			{
				_currentTurn = value;
				OnPropertyChanged("CurrentTurn");
			}
		}
		public int NbFleau
		{
			get { return _nbFleau; }
			set
			{
				_nbFleau = value;
				OnPropertyChanged("NbFleau");
			}
		}
		public List<JsonCardsParser.Card> OpponentPlayedCards
		{
			get
			{
				if (_opponentPlayedCards != null) return _opponentPlayedCards;
				_opponentPlayedCards = new List<Card>();
				return _opponentPlayedCards;
			}

			set
			{
				_opponentPlayedCards = value;
				OnPropertyChanged("OpponentPlayedCards");
			}
		}

		public List<DeckUI> CardAlreadyPlayed
		{
			get
			{
				if (_cardAlreadyPlayed != null) return _cardAlreadyPlayed;
				_cardAlreadyPlayed = new List<DeckUI>();
				return _cardAlreadyPlayed;
			}
			set
			{
				_cardAlreadyPlayed = value;
				OnPropertyChanged("CardAlreadyPlayed");

			}
		}

		public List<DeckUI> Deck
		{
			get
			{
				if (_deck != null) return _deck;
				_deck = new List<DeckUI>();
				return _deck;
			}
			set
			{
				_deck = value;
				OnPropertyChanged("Deck");
			}
		}


		public List<DeckUI> CardsInHand
		{
			get
			{
				if (_cardsInHand != null) return _cardsInHand;
				_cardsInHand = new List<DeckUI>();
				return _cardsInHand;
			}
			set
			{
				_cardsInHand = value;
				OnPropertyChanged("CardsInHand");
			}
		}

		public List<DeckUI> DeckInfinites
		{
			get
			{
				if (_deckInfinites != null) return _deck.Where(x => x.Card.Rarity == 4).ToList();
                _deckInfinites = new List<DeckUI>();
				return _deckInfinites;
			}
			set
			{
				_deckInfinites = value;
				OnPropertyChanged("DeckInfinites");
			}
		}

		public List<DeckUI> DeckKrosmiques
		{
			get
			{
				if (_deckKrosmiques != null) return _deck.Where(x => x.Card.Rarity == 3).ToList();
                _deckKrosmiques = new List<DeckUI>();
				return _deckKrosmiques;
			}
			set
			{
				_deckKrosmiques = value;
				OnPropertyChanged("DeckKrosmiques");
			}
		}

		#endregion

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);
		}

		public void AddCardToDeck(DeckUI card)
		{
			Deck.Add(card);
		}

		public void RemoveCardFromCardInHand(DeckUI card)
		{
			CardsInHand.Remove(card);
		}

		public void AddCardToCardInHand(DeckUI card)
		{
			CardsInHand.Add(card);
		}
	}
}