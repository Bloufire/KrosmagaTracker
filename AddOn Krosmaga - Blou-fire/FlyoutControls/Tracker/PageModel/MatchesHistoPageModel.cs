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
using AddOn_Krosmaga___Blou_fire.Services;
using AddOn_Krosmaga___Blou_fire.UIElements;
using MahApps.Metro.Controls;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class MatchesHistoPageModel : ObservableObject
	{
		private List<UIElements.Match> _filteredGames;
		public bool IsOpen { get; set; }
		#region CTOR

		public MatchesHistoPageModel() : base()
		{
			TrackerSrv.TrackerModel.PropertyChanged += _trackerSrv_PropertyChanged;
			if(TrackerSrv.CurrentFiltersStatModel != null)
			TrackerSrv.CurrentFiltersStatModel.PropertyChanged += CurrentFiltersStatModel_PropertyChanged; ;
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
			get
			{
				return _filteredGames?.OrderByDescending(x => x.Date).ToList(); 
				
			}

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
			var match = (Match)args;
			_filteredGames.Find(x => x.IdMatch == match.IdMatch).Deck.UpdateCardList();
		}



		#endregion

		public void _trackerSrv_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.FilteredGames)))
			UpdateScreen();
		}


		#region Add Deck To Compare
		private ICommand _addDeckToCompareList;
		public ICommand AddDeckToCompareList
		{
			get
			{

				if (_addDeckToCompareList == null)
					_addDeckToCompareList = new RelayCommand(OpenAdvancedStats);

				return _addDeckToCompareList;
			}
		}

		private void OpenAdvancedStats(object args)
		{

		
			//if (!IsOpen)
			//{
				////Get the clicked MenuItem
				//var menuItem = (MenuItem)sender;

				////Get the ContextMenu to which the menuItem belongs
				//var contextMenu = (ContextMenu)menuItem.Parent;

				////Find the placementTarget
				//var item = (DataGrid)contextMenu.PlacementTarget;

				//Get the underlying item, that you cast to your object that is bound
				//to the DataGrid (and has subject and state as property)
				var matchClicked = (Match)args;

				//On met à jour la propert CardList du Match
				matchClicked.Deck.UpdateCardList();

				//On prépare le Context
				var deckListPageModel = new DeckListPageModel();
				deckListPageModel.CardList.AddRange(matchClicked.Deck.CardsList);
				deckListPageModel.HeaderName = matchClicked.OpponentName;
				//On attache le context à la page
				DeckListPage ui = new DeckListPage { DataContext = deckListPageModel };


				MetroWindow newWindow = new MetroWindow
				{
					Height = 410,
					Width = 220,
					Title = $"Deck",
					Content = ui
				};
				newWindow.Show();
				newWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				newWindow.Topmost = true;
			//}
		}

	

		#endregion

	}
}