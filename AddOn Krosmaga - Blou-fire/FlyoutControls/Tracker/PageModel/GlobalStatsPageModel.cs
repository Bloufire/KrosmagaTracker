using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using AddOn_Krosmaga___Blou_fire.Data;
using AddOn_Krosmaga___Blou_fire.DataModel;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class GlobalStatsPageModel : ObservableObject
	{
		public GlobalStatsPageModel()
		{
		}


		#region Properties

		public IEnumerable<ChartStats> WinrateOverall
		{
			get
			{
				var games = TrackerSrv.TrackerModel.FilteredGames.ToList();
				var wins = games.Where(x => x.ResultatMatch == (int) GameResult.Win).ToList();
				
				return wins.Count > 0
					? wins.Select(x => new ChartStats {Name = "Wins", Value = Math.Round(100.0 * wins.Count() / games.Count)})
					: EmptyChartStats("Wins");
			}
		}
		public IEnumerable<ChartStats> AvgWinratePerClass
		=> TrackerSrv.TrackerModel.FilteredGames.ToList().GroupBy(x => x.PlayerClasse)
				
				.OrderBy(x => x.Key)
				.Select(x =>
						new ChartStats
						{
							Name = x.Key,
							Value = Math.Round(100.0 * x.Count(g => g.ResultatMatch == (int)GameResult.Win) / x.Count(), 1),
							Brush = new SolidColorBrush(Helpers.Helpers.GetClassColor(x.Key, true)),
							Class = StatsCore.GetKrosClassByName(x.Key)
						});
		
	
		public IEnumerable<ChartStats> WinratePerClassOverall
		{
			get
			{
				var games = TrackerSrv.TrackerModel.FilteredGames.ToList();
				var wins = games.Where(x => x.ResultatMatch == (int)GameResult.Win).ToList();

				return wins.Count > 0
					? wins.Select(x => new ChartStats { Name = "Wins", Value = Math.Round(100.0 * wins.Count() / games.Count) })
					: EmptyChartStats("Wins");
			}
		}



		private ChartStats[] EmptyChartStats(string name) => new[] {new ChartStats() {Name = name, Value = 0}};
		private string _titreDuChart;

		public string TitreDuChart
		{
			get { return _titreDuChart; }
			set
			{
				_titreDuChart = value;
				OnPropertyChanged("TitreDuChart");
			}
		}

		private string _ssTtitreDuChart;

		public string SsTitreDuChart
		{
			get { return _ssTtitreDuChart; }
			set
			{
				_ssTtitreDuChart = value;
				OnPropertyChanged("SsTitreDuChart");
			}
		}
		#endregion
	}
}