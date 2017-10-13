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

namespace AddOn_Krosmaga___Blou_fire.PageModels
{
	public class OverlayPageModel : ObservableObject
	{
		public OverlayPageModel()
		{
			SelectedContentItem = OverlayContentEnum.Tracker;
			Left = SystemParameters.PrimaryScreenWidth - 400;

		}

		#region Commands
		public ICommand BtnOpenOverlay_OnClick => new RelayCommand(BtnOpenOverlay);
		public ICommand BtnTrackerOnOverlay_OnClick => new RelayCommand(BtnOpenOverlayTracker);
		public ICommand BtnStatsOnOverlay_OnClick => new RelayCommand(BtnOpenOverlayStats);
		public ICommand BtnHistoOnOverlay_OnClick => new RelayCommand(BtnOpenOverlayHisto);
		public ICommand BtnSettingsOnOverlay_OnClick => new RelayCommand(BtnOpenOverlaySettings);
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

		private void BtnDiscordOnClick()
		{
			Helpers.Helpers.TryOpenUrl("https://discord.gg/UR2TQEp");
		}
		#endregion


		#region Properties
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
