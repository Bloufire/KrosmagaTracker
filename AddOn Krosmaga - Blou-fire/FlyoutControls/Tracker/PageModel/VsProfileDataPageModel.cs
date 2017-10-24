using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class VsProfileDataPageModel : ObservableObject
	{
		private int _vsLosesNb;
		private int _vsWinsNb;
		private int _vsNbFleau;
		private int _vsNbCardsInHand;
		private string _vsPseudo;
		private string _vsCurrentTurn;
		private int _vsRank;
		private bool _isRankVisible;

		#region CTOR
		public VsProfileDataPageModel()
		{
			TrackerSrv.TrackerModel.PropertyChanged += TrackerModel_PropertyChanged; ;
			UpdateScreen();
		}

		private void UpdateScreen()
		{
			VsPseudo = TrackerSrv.TrackerModel.VsPseudo;
			VsWinsNb = TrackerSrv.TrackerModel.VsWinsNb;
			VsNbFleau = TrackerSrv.TrackerModel.NbFleau;
			VsLosesNb = TrackerSrv.TrackerModel.VsLosesNb;
			VsNbCardsInHand = TrackerSrv.TrackerModel.OpponentCardsInHand;
			VsRank = TrackerSrv.TrackerModel.OpponentLevel;
			VsCurrentTurn = "Turn " + TrackerSrv.TrackerModel.CurrentTurn.ToString();

		}

		private void TrackerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.VsPseudo))
				|| e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.VsWinsNb))
			   || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.NbFleau))
			   || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.VsLosesNb))
			   || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.OpponentCardsInHand))
			   || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.OpponentLevel))
			   || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.CurrentTurn)))
			UpdateScreen();
		}
		#endregion

		#region Properties 

		public int VsLosesNb
		{
			get { return _vsLosesNb; }
			set
			{
				_vsLosesNb = value;
				OnPropertyChanged(nameof(VsLosesNb));
			}
		}

		public bool IsRankVisible
		{
			get { return _isRankVisible; }
			set
			{
				_isRankVisible = value;
				OnPropertyChanged(nameof(IsRankVisible));
			}
		}

		public int VsRank
		{
			get
			{
				return _vsRank;
				
			}
			set
			{
				_vsRank = value;
				IsRankVisible = VsRank != 0;
				OnPropertyChanged(nameof(VsRank));
			}
		}

		public int VsWinsNb
		{
			get { return _vsWinsNb; }
			set
			{
				_vsWinsNb = value;
				OnPropertyChanged(nameof(VsWinsNb));
			}
		}
		public int VsNbFleau
		{
			get { return _vsNbFleau; }
			set
			{
				_vsNbFleau = value;
				OnPropertyChanged(nameof(VsNbFleau));
			}
		}

		public int VsNbCardsInHand
		{
			get { return _vsNbCardsInHand; }
			set
			{
				_vsNbCardsInHand = value;
				OnPropertyChanged(nameof(VsNbCardsInHand));
			}
		}

		public string VsPseudo
		{
			get
			{
				return string.IsNullOrEmpty(_vsPseudo) ? "     " : _vsPseudo;
			}
			set
			{
				_vsPseudo = value;
				OnPropertyChanged(nameof(VsPseudo));
			}
		}

		public string VsCurrentTurn
		{
			get { return _vsCurrentTurn; }
			set
			{
				_vsCurrentTurn = value;
				OnPropertyChanged(nameof(VsCurrentTurn));
			}
		}

		#endregion
	}
}
