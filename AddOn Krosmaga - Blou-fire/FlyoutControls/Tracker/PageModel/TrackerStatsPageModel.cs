using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Helpers;
using MahApps.Metro;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class TrackerStatsPageModel : ObservableObject
	{
		public TrackerStatsPageModel()
		{
			ComboSaisonValues = Enum.GetValues(typeof(SaisonsEnum));
			ComboClasseValues = Enum.GetValues(typeof(ClassEnum));
			ComboOppClasseValues = Enum.GetValues(typeof(ClassEnum));
		}

		#region properties

		public Array ComboSaisonValues { get; set; }
		public Array ComboClasseValues { get; set; }
		public Array ComboOppClasseValues { get; set; }
		#endregion

		#region Commands

		#region SaisonSelection

		public ICommand ComboBoxSaisonOnSelectionChanged => new RelayCommand(ComboBoxSaison_OnSelectionChangedAction);

		private string _selectedItemSaison;
		public string SelectedItemSaison
		{
			get => this._selectedItemSaison;
			set
			{
				this._selectedItemSaison = value;
				OnPropertyChanged("SelectedItemSaison");
			}
		}
		private void ComboBoxSaison_OnSelectionChangedAction()
		{
			if (_selectedItemSaison != null)
			{
				Debug.WriteLine("SelectedItemSaison = " + _selectedItemSaison);
			}
		}

		#endregion
		#region OppClasseSelection

		public ICommand ComboBoxOppClasseOnSelectionChanged => new RelayCommand(ComboBoxOppClasse_OnSelectionChangedAction);

		private string _selectedItemOppClasse;
		public string SelectedItemOppClasse
		{
			get => this._selectedItemOppClasse;
			set
			{
				this._selectedItemOppClasse = value;
				OnPropertyChanged("SelectedItemOppClasse");
			}
		}
		private void ComboBoxOppClasse_OnSelectionChangedAction()
		{
			if (_selectedItemOppClasse != null)
			{
				Debug.WriteLine("SelectedItemOppClasse = " + _selectedItemOppClasse);
			}
		}

		#endregion
		#region ClasseSelection

		public ICommand ComboBoxClasseOnSelectionChanged => new RelayCommand(ComboBoxClasse_OnSelectionChangedAction);

		private string _selectedItemClasse;
		public string SelectedItemClasse
		{
			get => this._selectedItemClasse;
			set
			{
				this._selectedItemClasse = value;
				OnPropertyChanged("SelectedItemClasse");
			}
		}
		private void ComboBoxClasse_OnSelectionChangedAction()
		{
			if (_selectedItemClasse != null)
			{
				Debug.WriteLine("SelectedItemClasse = " + _selectedItemClasse);
			}
		}

		#endregion

		#endregion
	}
}
