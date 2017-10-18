using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.Models
{
	public class FiltersStatModel : ObservableObject
	{
		private SaisonsEnum _selectedSaison;
		private ClassEnum _selectedClass;
		private ClassEnum _selectedVsClass;
		private GameType _selectedGameType;
		private bool _isDetailWinrateDisplayedAsPercent;

		public FiltersStatModel()
		{
			
		}
		public FiltersStatModel(SaisonsEnum selectedSaison, ClassEnum selectedClass, ClassEnum selectedVsClass,GameType selectedGameType)
		{
			SelectedSaison = selectedSaison;
			SelectedClass = selectedClass;
			SelectedVsClass = selectedVsClass;
			SelectedGameType = selectedGameType;
		}

		#region Properties

		public SaisonsEnum SelectedSaison
		{
			get { return _selectedSaison; }
			set
			{
				_selectedSaison = value;
				OnPropertyChanged("SelectedSaison");
			}
		}

		public ClassEnum SelectedClass
		{
			get { return _selectedClass; }
			set
			{
				_selectedClass = value;
				OnPropertyChanged("SelectedClass");
			}
		}
		public GameType SelectedGameType
		{
			get { return _selectedGameType; }
			set
			{
				_selectedGameType = value;
				OnPropertyChanged("SelectedGameType");
			}
		}
		public ClassEnum SelectedVsClass
		{
			get { return _selectedVsClass; }
			set
			{
				_selectedVsClass = value;
				OnPropertyChanged("SelectedVsClass");
			}
		}

		public bool IsDetailWinrateDisplayedAsPercent
		{
			get { return _isDetailWinrateDisplayedAsPercent; }
			set
			{
				_isDetailWinrateDisplayedAsPercent = value;
				OnPropertyChanged("IsDetailWinrateDisplayedAsPercent");
			}
		}

		#endregion
	}
}
