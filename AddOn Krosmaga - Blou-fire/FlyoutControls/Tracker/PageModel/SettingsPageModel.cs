using System.Windows;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;
using MahApps.Metro;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class SettingsPageModel : ObservableObject
	{
		public SettingsPageModel()
		{
			//Association des commands ici
			AccentSelectionChanged = new RelayCommand(ChangeAppStyle);
			//ColorsSelectorOnSelectionChanged = new RelayCommand(ColorsSelectorOnSelectionChangedAction);
			ThemeSelectionChanged = new RelayCommand(ChangeAppStyle);
		}

		#region Properties

		#endregion

		#region Commands

		#region AccentSelection

		public ICommand AccentSelectionChanged { get; }

		private Accent _selectedItemAccent;
		public Accent SelectedItemAccent
		{
			get => this._selectedItemAccent;
			set
			{
				this._selectedItemAccent = value;
				OnPropertyChanged("SelectedItemAccent");
			}
		}

		#endregion

		#region ThemeSelection

		public ICommand ThemeSelectionChanged { get; }

		private bool _darkAndLightThemeBool = true;

		public bool DarkAndLightThemeBool
		{
			get => _darkAndLightThemeBool;
			set
			{
				this._darkAndLightThemeBool = value;
				OnPropertyChanged("DarkAndLightThemeBool");
			}

		}

		#endregion
		#endregion

		public void ChangeAppStyle()
		{
			// set the Red accent and dark theme only to the current window
			ThemeManager.ChangeAppStyle(Application.Current,
				ThemeManager.GetAccent(SelectedItemAccent.Name),
				ThemeManager.GetAppTheme(_darkAndLightThemeBool == true ? "basedark" :"baselight"));
		}
	}
}
