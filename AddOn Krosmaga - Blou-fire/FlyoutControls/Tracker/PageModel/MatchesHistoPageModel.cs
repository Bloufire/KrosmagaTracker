using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Models;
using AddOn_Krosmaga___Blou_fire.OverlayContainer.PageModels;
using AddOn_Krosmaga___Blou_fire.OverlayContainer.Pages;
using AddOn_Krosmaga___Blou_fire.Services;
using AddOn_Krosmaga___Blou_fire.UIElements;
using MahApps.Metro.Controls;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class MatchesHistoPageModel : ObservableObject
	{
		private List<UIElements.Match> _filteredGames;
		private readonly DecksComparerPageModel decksComparerPageModel = new DecksComparerPageModel();
		public bool IsOpen
		{
			get { return _isOpen; }
			set
			{
				_isOpen = value;
				OnPropertyChanged("IsOpen");
			}
		}

		#region CTOR

		public MatchesHistoPageModel() : base()
		{
			TrackerSrv.TrackerModel.PropertyChanged += _trackerSrv_PropertyChanged;
			if (TrackerSrv.CurrentFiltersStatModel != null)
				TrackerSrv.CurrentFiltersStatModel.PropertyChanged += CurrentFiltersStatModel_PropertyChanged;
			;
			TrackerSrv.UpdateMatchsWithFilterList();
			UpdateScreen();
		}

		private void CurrentFiltersStatModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			TrackerSrv.UpdateMatchsWithFilterList();
		}



		private void UpdateScreen()
		{
			FilteredGames = TrackerSrv.TrackerModel.FilteredGames;
		}

		#endregion

		#region Properties

		public List<UIElements.Match> FilteredGames
		{
			get { return _filteredGames?.OrderByDescending(x => x.Date).ToList(); }

			set
			{
				_filteredGames = value;
				OnPropertyChanged("FilteredGames");
			}
		}

		#endregion

		#region Commands

		private ICommand _loadDataRowDetailShow;

		public ICommand LoadDataRowDetailShowCmd
		{
			get
			{

				if (_loadDataRowDetailShow == null)
					_loadDataRowDetailShow = new RelayCommand(this.LoadDataRowDetailShow);

				return _loadDataRowDetailShow;
			}
		}

		private void LoadDataRowDetailShow(object args)
		{
			var match = (Match) args;
			_filteredGames.Find(x => x.IdMatch == match.IdMatch).Deck.UpdateCardList();
		}



		#endregion

		public void _trackerSrv_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.FilteredGames)))
				UpdateScreen();
		}


		#region Show Deck To Compare

		private ICommand _showDeckToCompareList;

		public ICommand ShowDeckCommand
		{
			get
			{

				if (_showDeckToCompareList == null)
					_showDeckToCompareList = new RelayCommand(ShowDeck);

				return _showDeckToCompareList;
			}
		}

		private void ShowDeck(object args)
		{


			if (!IsOpen)
			{
				////Get the clicked MenuItem
				//var menuItem = (MenuItem)sender;

				////Get the ContextMenu to which the menuItem belongs
				//var contextMenu = (ContextMenu)menuItem.Parent;

				////Find the placementTarget
				//var item = (DataGrid)contextMenu.PlacementTarget;

				//Get the underlying item, that you cast to your object that is bound
				//to the DataGrid (and has subject and state as property)
				var matchClicked = (Match) args;

				//On met à jour la propert CardList du Match
				matchClicked.Deck.UpdateCardList();

				//On prépare le Context
				var deckListPageModel = new DeckListPageModel();
				deckListPageModel.CardList = matchClicked.Deck.CardsList;
		
				deckListPageModel.HeaderName = matchClicked.OpponentName;
				//On attache le context à la page
				
				decksComparerPageModel.DeckAModel = deckListPageModel;

				DecksComparerPage ui = new DecksComparerPage() {DataContext = decksComparerPageModel };


				MetroWindow newWindow = new MetroWindow
				{
					Height = 700,
					Width = 500,
					Title = $"Deck",
					Content = ui
				};
				newWindow.Loaded += NewWindow_Loaded;
				newWindow.Closing += NewWindow_Closing;
				newWindow.Show();
				newWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				newWindow.Topmost = true;
			}
		}

		private void NewWindow_Closing(object sender, CancelEventArgs e)
		{
			IsOpen = false;
		}

		private void NewWindow_Loaded(object sender, RoutedEventArgs e)
		{
			IsOpen = true;
		}



		#endregion

		#region ReplaceDeckACommand

		private ICommand _replaceDeckAToCompare;

		public ICommand ReplaceDeckACommand
		{
			get
			{
				if (_replaceDeckAToCompare == null)
					_replaceDeckAToCompare = new RelayCommand(ReplaceDeckA);

				return _replaceDeckAToCompare;
			}
		}

		private void ReplaceDeckA(object args)
		{
			var matchClicked = (Match)args;

			//On met à jour la propert CardList du Match
			matchClicked.Deck.UpdateCardList();
			var deckListPageModel = new DeckListPageModel();
			deckListPageModel.CardList=matchClicked.Deck.CardsList;
			
			deckListPageModel.HeaderName = matchClicked.OpponentName;
			decksComparerPageModel.DeckAModel = deckListPageModel;
		}

		#endregion

		#region ReplaceDeckACommand

		private ICommand _replaceDeckBToCompare;
		private bool _isOpen;

		public ICommand ReplaceDeckBCommand
		{
			get
			{

				if (_replaceDeckBToCompare == null)
					_replaceDeckBToCompare = new RelayCommand(ReplaceDeckB);

				return _replaceDeckBToCompare;
			}
		}

		private void ReplaceDeckB(object args)
		{
			var matchClicked = (Match)args;

			//On met à jour la propert CardList du Match
			matchClicked.Deck.UpdateCardList();
			var deckListPageModel = new DeckListPageModel();
			deckListPageModel.CardList = matchClicked.Deck.CardsList;
			
			deckListPageModel.HeaderName = matchClicked.OpponentName;
			decksComparerPageModel.DeckBModel = deckListPageModel;
		}

		#endregion
	}
}