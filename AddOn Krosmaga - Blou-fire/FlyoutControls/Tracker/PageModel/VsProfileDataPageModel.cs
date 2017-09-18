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
			VsLosesNb = TrackerSrv.TrackerModel.VsLosesNb;
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
