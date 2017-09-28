using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.PageModels
{
	public class OverlayPageModel : ObservableObject
	{
		public OverlayPageModel()
		{
				}


		public ICommand BtnSettings_OnClick => new RelayCommand(BtnSettingsOnClick);

		private const int WINDOWOPENED = 1000;
		private const int WINDOWCLOSED = 40;

		private void BtnSettingsOnClick()
		{
			if(OverlayWindowsWidth == WINDOWOPENED)
			OverlayWindowsWidth = WINDOWCLOSED;
			else
			{
				OverlayWindowsWidth = WINDOWOPENED;
			}

			IsLeftFlyOpen = !IsLeftFlyOpen;
		}
		private bool _isLeftFlyOpen;
		public bool IsLeftFlyOpen
		{
			get => this._isLeftFlyOpen;
			set
			{
				this._isLeftFlyOpen = value;
				OnPropertyChanged("IsLeftFlyOpen");
			}
		}

		private int _overlayWindowsWidth;

		public int OverlayWindowsWidth
		{
			get { return _overlayWindowsWidth == 0 ? WINDOWCLOSED : _overlayWindowsWidth; }
			set
			{
				_overlayWindowsWidth = value;
				OnPropertyChanged("OverlayWindowsWidth");
			}
		}


	}
}
