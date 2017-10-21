using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Properties;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AddOn_Krosmaga___Blou_fire.PageModels
{
	public class OverlayPageModel : ObservableObject
	{
		public OverlayPageModel()
		{
			SelectedContentItem = OverlayContentEnum.Tracker;
			Left = SystemParameters.PrimaryScreenWidth - Resources.OverlayWidth;

		}

		#region Commands
		public ICommand BtnOpenOverlay_OnClick => new RelayCommand(BtnOpenOverlay);
	    public ICommand BtnTrackerOnOverlay_OnClick => new RelayCommand(BtnOpenOverlayTracker);
		public ICommand BtnStatsOnOverlay_OnClick => new RelayCommand(BtnOpenOverlayStats);
		public ICommand BtnHistoOnOverlay_OnClick => new RelayCommand(BtnOpenOverlayHisto);
		public ICommand BtnSettingsOnOverlay_OnClick => new RelayCommand(BtnOpenOverlaySettings);
		
		public ICommand BtnOpenContactOverlay_OnClick => new RelayCommand(BtnOpenContactOverlaySettings);

		public ICommand BtnDiscord_OnClick { get; }



		private void BtnOpenOverlay()
		{
		
			IsLeftFlyOpen = !IsLeftFlyOpen;
		}

      



        private void BtnOpenOverlayTracker()
		{
			SelectedContentItem = OverlayContentEnum.Tracker;
			IsLeftFlyOpen = true;
		}
		private void BtnOpenOverlayStats()
		{
			SelectedContentItem = OverlayContentEnum.Stats;
			IsLeftFlyOpen = true;
		}
		private void BtnOpenOverlayHisto()
		{
			SelectedContentItem = OverlayContentEnum.Histo;
			IsLeftFlyOpen = true;
		}
		private void BtnOpenOverlaySettings()
		{
			SelectedContentItem = OverlayContentEnum.Settings;
			IsLeftFlyOpen = true;
		}
		
		private void BtnOpenContactOverlaySettings()
		{
			SelectedContentItem = OverlayContentEnum.ContactUs;
			IsLeftFlyOpen = true;
		}
		private void BtnDiscordOnClick()
		{
			Helpers.Helpers.TryOpenUrl("https://discord.gg/UR2TQEp");
		}
        #endregion


        #region Properties

	    private ICommand _closeOverlayCommand;
	    public ICommand CloseOverlay
	    {
	        get
	        {

	            if (_closeOverlayCommand == null)
	                _closeOverlayCommand = new AsyncCommand(CloseOverlayNow);

	            return _closeOverlayCommand;
	        }
	    }

	    private async Task CloseOverlayNow()
	    {
			var metroWindow = (Application.Current.MainWindow as MetroWindow);
		    var res = await metroWindow.ShowMessageAsync("Quit Kros'Tracker", "Are you sure you want to exit ?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings()
			    {
				    DialogTitleFontSize = 15,
				    DialogMessageFontSize = 13,
				    AffirmativeButtonText = "Yes",
				    NegativeButtonText = "No",
				    DefaultButtonFocus = MessageDialogResult.Affirmative

			    }
		    );

		    if (res == MessageDialogResult.Affirmative)
			    System.Windows.Application.Current.Shutdown();
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

		private OverlayContentEnum _selectedContentItem;

		public OverlayContentEnum SelectedContentItem
		{
			get { return _selectedContentItem; }
			set
			{
				_selectedContentItem = value;
				OnPropertyChanged("SelectedContentItem");
			}
		}


		private double winLeft;

		public double Left
		{
			get { return winLeft; }
			set
			{
			
				winLeft = value;
				OnPropertyChanged("Left");
			}
		}

		private double winTop;

		public double Top
		{
			get { return winTop; }
			set { winTop = value; }
		}

        #endregion

	  
    }
}
