using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;
using MahApps.Metro.Controls;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
    public class AdvancedStatsPageModel : ObservableObject
    {

	    public AdvancedStatsPageModel()
	    {
		    
	    }

	    private ICommand _openAdvancedStats;
	    public ICommand OpenAdvancedStatsWindow
		{
		    get
		    {

			    if (_openAdvancedStats == null)
				    _openAdvancedStats = new RelayCommand(OpenAdvancedStats);

			    return _openAdvancedStats;
		    }
	    }

	    private void OpenAdvancedStats()
	    {
		    if (!IsOpen)
		    {
			    AdvancedStatsContentPage ui = new AdvancedStatsContentPage();
			    MetroWindow newWindow = new MetroWindow();
			    newWindow.Height = 410;
			    newWindow.Width = 580;
			    newWindow.Title = "Matchup Details";
			    newWindow.Content = ui;
			    newWindow.Loaded += NewWindow_Loaded;
			    newWindow.Closing += NewWindow_Closing;
			    newWindow.Show();
		    }
	    }

		private void NewWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			IsOpen = false;
		}

		private void NewWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
				IsOpen = true;
		}

	    public bool IsOpen { get; set; }
    }
}
