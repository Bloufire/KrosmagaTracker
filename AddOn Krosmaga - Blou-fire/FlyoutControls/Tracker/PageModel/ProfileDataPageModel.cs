using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Services;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class ProfileDataPageModel : ObservableObject
	{
		#region constructor

		public ProfileDataPageModel()
		{
			TrackerSrv.TrackerModel.PropertyChanged += _trackerSrv_PropertyChanged;
			UpdateScreen();
		}

		private void _trackerSrv_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			UpdateScreen();
		}

		private void UpdateScreen()
		{
			MyPseudo = TrackerSrv.TrackerModel.OwnPseudo;
			MyWinsNb = TrackerSrv.TrackerModel.OwnWinsNb;
			MyLosesNb = TrackerSrv.TrackerModel.OwnLosesNb;
		}

		#endregion

		#region Properties

		private string _myPseudo;
		

		public string MyPseudo
		{
			get { return _myPseudo ?? "Pseudo == Null"; }

			set
			{
				_myPseudo = value;
				OnPropertyChanged("MyPseudo");
			}
		}

		#endregion

		#region OwnWinsNb

		private int _ownWinsNb;

		public int MyWinsNb
		{
			get => _ownWinsNb;

			set
			{
				_ownWinsNb = value;
				OnPropertyChanged("MyWinsNb");
			}
		}

		#endregion

		private int _ownLosesNb;

		public int MyLosesNb
		{
			get => _ownLosesNb;

			set
			{
				_ownLosesNb = value;
				OnPropertyChanged("MyLosesNb");
			}
		}

		#region Commands

		#endregion
	}
}