using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
    public class DofusTrackerControlPageModel : ObservableObject
	{
		private RelayCommand _menuSelectionChanged;

		public DofusTrackerControlPageModel()
		{
			
		}
		private int _numValue;

		public int NumValue
		{
			get { return _numValue; }
			set
			{
				_numValue = value;
				OnPropertyChanged(nameof(NumValue));
			}
		}

		private ICommand _addCounter;
		public ICommand AddOneCounter
		{
			get
			{

				if (_addCounter == null)
					_addCounter = new RelayCommand(AddOneToDofus);

				return _addCounter;
			}
		}
		private ICommand _subCounter;
		public ICommand SubOneCounter
		{
			get
			{

				if (_subCounter == null)
					_subCounter = new RelayCommand(SubstractOneToDofus);

				return _subCounter;
			}
		}
		private void AddOneToDofus()
		{
			NumValue++;
		}
		private void SubstractOneToDofus()
		{
			NumValue--;
		}
	}
}
