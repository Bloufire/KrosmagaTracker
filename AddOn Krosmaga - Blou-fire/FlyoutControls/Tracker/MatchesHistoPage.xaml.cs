using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel;
using JsonCardsParser;
using MahApps.Metro.Controls;
using SQLiteConnector;
using Match = AddOn_Krosmaga___Blou_fire.UIElements.Match;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker
{
	/// <summary>
	/// Logique d'interaction pour MatchesHistoPage.xaml
	/// </summary>
	public partial class MatchesHistoPage
	{
		public MatchesHistoPage()
		{
			InitializeComponent();
		}


		private void MenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			//Get the clicked MenuItem
			var menuItem = (MenuItem)sender;
		
			//Get the ContextMenu to which the menuItem belongs
			var contextMenu = (ContextMenu)menuItem.Parent;

			//Find the placementTarget
			var item = (DataGrid)contextMenu.PlacementTarget;

			//Get the underlying item, that you cast to your object that is bound
			//to the DataGrid (and has subject and state as property)
			var matchClicked = (Match)item.SelectedCells[0].Item;

			//On met à jour la propert CardList du Match
			matchClicked.Deck.UpdateCardList();

			//On prépare le Context
			var deckListPageModel = new DeckListPageModel();
			deckListPageModel.CardList.AddRange(matchClicked.Deck.CardsList);
			deckListPageModel.HeaderName = matchClicked.OpponentName;
			//On attache le context à la page
			DeckListPage ui = new DeckListPage {DataContext = deckListPageModel };


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
		}
	}
}