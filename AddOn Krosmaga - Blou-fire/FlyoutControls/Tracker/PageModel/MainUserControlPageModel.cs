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

	

		#endregion

	}
}
