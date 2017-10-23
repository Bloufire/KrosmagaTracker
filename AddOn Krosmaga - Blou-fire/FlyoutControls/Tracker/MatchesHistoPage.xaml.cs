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
using MahApps.Metro.Controls;

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


			AdvancedStatsContentPage ui = new AdvancedStatsContentPage();
			MetroWindow newWindow = new MetroWindow();
			newWindow.Height = 410;
			newWindow.Width = 580;
			newWindow.Title = "Matchup Details";
			newWindow.Content = ui;
			newWindow.Show();
		}
	}
}