using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Services;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class MatchesHistoPageModel : ObservableObject
	{
		private GameResult _result;
		private List<UIElements.Match> _filteredGames;
		private TrackerCoreSrv _trackerSrv;




		#region CTOR
		public MatchesHistoPageModel()
		{
			App myApplication = ((App)Application.Current);
			_trackerSrv = myApplication.TrackerCoreService;
			_trackerSrv.TrackerModel.PropertyChanged += _trackerSrv_PropertyChanged;
			UpdateScreen();
		}

		private void _trackerSrv_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			UpdateScreen();
		}

		private void UpdateScreen()
		{
			FilteredGames = _trackerSrv.TrackerModel.FilteredGames;
		}

		#endregion

		#region Properties

		public GameResult Result
		{
			get { return _result; }
			set
			{
				_result = value;
				OnPropertyChanged(nameof(Result));
			}
		}

		public List<UIElements.Match> FilteredGames
		{
			get
			{

				return _filteredGames?.OrderByDescending(x => x.Date).ToList();
			}

			set
			{
				_filteredGames = value;
				OnPropertyChanged("FilteredGames");
			}
		}


		[XmlIgnore]
		public SolidColorBrush ResultTextColor
		{
			get
			{
				var c = Colors.Black;
				if (Result == GameResult.Win)
					c = Colors.Green;
				else if (Result == GameResult.Loss)
					c = Colors.Red;
				return new SolidColorBrush(c);
			}
		}



		#endregion

		#region Commands



		#endregion
	}
}
