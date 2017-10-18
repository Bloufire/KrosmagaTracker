using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Data;
using AddOn_Krosmaga___Blou_fire.DataModel;
using AddOn_Krosmaga___Blou_fire.Enums;
using MahApps.Metro.Controls;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class AdvancedStatsContentPageModel : ObservableObject
	{
		public List<KrosClass> ComboClasseValues { get; set; }
		public AdvancedStatsContentPageModel()
		{

			ComboClasseValues = StatsCore.GetAllClassAndImage();
			Matchups = new List<MatchUpData>();
			IsDisplayAsPercent = false;
			foreach (var classe in ComboClasseValues)
			{
				ClassEnum.TryParse(classe.NameClass, out ClassEnum myClassPick);
				var newMatchup = new MatchUpData(myClassPick);
				//newMatchup.CalculAllMatchup(myClassPick);
				_matchups.Add(newMatchup);
			}

		}
		private List<MatchUpData> _matchups;

		public List<MatchUpData> Matchups
		{
			get { return _matchups; }
			set { _matchups = value;
				OnPropertyChanged("Matchups"); }
		}





		private ICommand _swapDisplayWinrateModeCmd;
		private bool _isDisplayAsPercent;

		public ICommand SwapDisplayWinrateModeCmd
		{
			get
			{

				if (_swapDisplayWinrateModeCmd == null)
					_swapDisplayWinrateModeCmd = new RelayCommand(SwapDisplayWinrateMode);

				return _swapDisplayWinrateModeCmd;
			}
		}

		private void SwapDisplayWinrateMode()
		{
		 TrackerSrv.CurrentFiltersStatModel.IsDetailWinrateDisplayedAsPercent = !TrackerSrv.CurrentFiltersStatModel.IsDetailWinrateDisplayedAsPercent;
			IsDisplayAsPercent = !IsDisplayAsPercent;
		}

		public bool IsDisplayAsPercent
		{
			get { return _isDisplayAsPercent; }
			set
			{
				_isDisplayAsPercent = value;
				OnPropertyChanged("IsDisplayAsPercent");
			}
		}
	}


}
