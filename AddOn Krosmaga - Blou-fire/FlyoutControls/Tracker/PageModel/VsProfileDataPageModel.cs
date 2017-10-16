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
			VsNbCardsInHand = TrackerSrv.TrackerModel.CardsInHand.Count;
			VsNbFleau = TrackerSrv.TrackerModel.NbFleau;
			VsLosesNb = TrackerSrv.TrackerModel.VsLosesNb;
			VsNbCardsInHand = TrackerSrv.TrackerModel.OpponentCardsInHand;
		}

		private void TrackerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			UpdateScreen();
		}
		#endregion

		#region Properties 

		public int VsLosesNb
		{
			get { return _vsLosesNb; }
			set { _vsLosesNb = value;
				OnPropertyChanged(nameof(VsLosesNb));
			}
		}

		public int VsWinsNb
		{
			get { return _vsWinsNb; }
			set { _vsWinsNb = value;
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
			get { return _vsPseudo; }
			set
			{
				_vsPseudo = value;
				OnPropertyChanged(nameof(VsPseudo));
			}
		}

		#endregion
	}
}
