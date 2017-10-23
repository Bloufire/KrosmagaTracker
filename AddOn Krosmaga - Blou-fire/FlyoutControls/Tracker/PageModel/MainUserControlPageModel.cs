using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class MainUserControlPageModel : ObservableObject
	{
		public MainUserControlPageModel()
		{
			TrackerSrv.PropertyChanged += TrackerSrv_PropertyChanged;
		

		}

		private void TrackerSrv_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			
		}

		private void SetNewContent(object args)
		{
			var selectedItem = (TreeViewItem) args;
			if (selectedItem.Parent.GetType() == typeof(TreeViewItem)) // verify that parent is TreeViewItem
			{
				TreeViewItem parent = (TreeViewItem)selectedItem.Parent;
				switch ($"{parent.Header}_{selectedItem.Header}")
				{
					case "Arena_Summary":
						SelectedMenuFilterStat = null;
						SelectedMenuStatChartContent = null;
						break;
					case "Arena_Runs":
						SelectedMenuFilterStat = null;
						SelectedMenuStatChartContent = null;
						break;
					case "Constructed_Summary":
						SelectedMenuFilterStat = new TrackerStats();
						SelectedMenuStatChartContent = new MatchesHistoPage();
						break;
					case "Constructed_Matches":
						SelectedMenuFilterStat = null;
						SelectedMenuStatChartContent = null;
						break;
					case "Constructed_Charts":
						SelectedMenuFilterStat = null;
						SelectedMenuStatChartContent = null;
						break;
				}
				

			}

		}

	
		private UserControl selectedMenuFilterStat;
		private UserControl selectedMenuStatChartContent;

		#region MenuSelection
		private ICommand _menuSelectionChanged;
		public ICommand MenuSelectionChanged
		{
			get
			{

				if (_menuSelectionChanged == null)
					_menuSelectionChanged = new RelayCommand(this.SetNewContent);

				return _menuSelectionChanged;
			}
		}

		#region Content Properties
		public UserControl SelectedMenuStatChartContent
		{
			get { return selectedMenuStatChartContent; }
			set
			{
				selectedMenuStatChartContent = value;
				OnPropertyChanged(nameof(SelectedMenuStatChartContent));
			}
		}
		public UserControl SelectedMenuFilterStat
		{
			get { return selectedMenuFilterStat; }
			set
			{
				selectedMenuFilterStat = value;
				OnPropertyChanged(nameof(SelectedMenuFilterStat));
			}
		} 
		#endregion

		#endregion

	}
}
