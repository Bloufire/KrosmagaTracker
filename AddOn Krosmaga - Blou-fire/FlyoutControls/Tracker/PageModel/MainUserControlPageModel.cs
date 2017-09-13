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
			MenuSelectionChanged = new RelayCommand(SetNewContent);

		}

		private void TrackerSrv_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			SetNewContent();
		}

		private void SetNewContent()
		{
			if (SelectedMenuStat.Parent.GetType() == typeof(TreeViewItem)) // verify that parent is TreeViewItem
			{
				TreeViewItem parent = (TreeViewItem)SelectedMenuStat.Parent;
				switch ($"{parent.Header}_{SelectedMenuStat.Header}")
				{
					case "Arena_Summary":
						SelectedMenuFilterStat = new TrackerStats();
						break;
					case "Arena_Runs":
						break;
					case "Constructed_Summary":
						break;
					case "Constructed_Matches":
						break;
					case "Constructed_Charts":
						break;
				}
				

			}

		}

		private UserControl selectedMenuFilterStat;

		public UserControl SelectedMenuFilterStat
		{
			get { return selectedMenuFilterStat; }
			set
			{
				selectedMenuFilterStat = value;
				OnPropertyChanged(nameof(SelectedMenuFilterStat));
			}
		}

		#region MenuSelection

		public ICommand MenuSelectionChanged { get; }

		private TreeViewItem _selectedMenuStat;
		public TreeViewItem SelectedMenuStat
		{
			get => this._selectedMenuStat;
			set
			{
				this._selectedMenuStat = value;
				OnPropertyChanged("SelectedMenuStat");
			}
		}

		#endregion

	}
}
