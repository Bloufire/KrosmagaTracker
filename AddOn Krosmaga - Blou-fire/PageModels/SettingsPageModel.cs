using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AddOn_Krosmaga___Blou_fire.Helpers;
using MahApps.Metro;

namespace AddOn_Krosmaga___Blou_fire.PageModels
{
	public class SettingsPageModel : ObservableObject
	{
		public SettingsPageModel()
		{
			//Association des commands ici
			AccentSelectionChanged = new RelayCommand(AccentSelectionChangedAction);
			//ColorsSelectorOnSelectionChanged = new RelayCommand(ColorsSelectorOnSelectionChangedAction);
			ThemeSelectionChanged = new RelayCommand(ThemeSelectionChangedAction);
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

		private void AccentSelectionChangedAction()
		{
			if (_selectedItemAccent != null)
			{
				var theme = ThemeManager.DetectAppStyle(Application.Current);
				ThemeManager.ChangeAppStyle(Application.Current, _selectedItemAccent, theme.Item1);
				Application.Current.MainWindow.Activate();
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

		private void ThemeSelectionChangedAction()
		{
			var theme = ThemeManager.DetectAppStyle(Application.Current);
			ThemeManager.ChangeAppStyle(Application.Current, theme.Item2,
				_darkAndLightThemeBool == true ? ThemeManager.GetAppTheme("basedark") : ThemeManager.GetAppTheme("baselight"));
			Application.Current.MainWindow.Activate();
		}

		#endregion

		#endregion
	}
}