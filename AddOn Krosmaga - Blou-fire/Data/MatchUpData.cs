using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Services;
using AddOn_Krosmaga___Blou_fire.UIElements;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	public class MatchUpData : ObservableObject
	{
		protected static TrackerCoreSrv TrackerSrv;
		private double _iopWinrateMu;
		private double _craWinrateMu;
		private double _eniWinrateMu;
		private double _ecaWinrateMu;
		private double _enuWinrateMu;
		private double _sramWinrateMu;
		private double _xelWinrateMu;
		private double _sacriWinrateMu;
		private double _sadiWinrateMu;

		public MatchUpData()
		{
			App myApplication = ((App)Application.Current);
			TrackerSrv = myApplication.TrackerCoreService;
			CalculAllMatchup(ClassEnum.Sadida);
		}

		public double IopWinrateMU
		{
			get { return _iopWinrateMu; }
			set
			{
				_iopWinrateMu = value;
				OnPropertyChanged("IopWinrateMU");
			}
		}

		public double CraWinrateMU
		{
			get { return _craWinrateMu; }
			set
			{
				_craWinrateMu = value;
				OnPropertyChanged("CraWinrateMU");
			}
		}

		public double EniWinrateMU
		{
			get { return _eniWinrateMu; }
			set { _eniWinrateMu = value; OnPropertyChanged("EniWinrateMU");
			}
		}

		public double EcaWinrateMU
		{
			get { return _ecaWinrateMu; }
			set
			{
				_ecaWinrateMu = value;
				OnPropertyChanged("EcaWinrateMU");
			}
		}

		public double EnuWinrateMU
		{
			get { return _enuWinrateMu; }
			set
			{
				_enuWinrateMu = value;
				OnPropertyChanged("EnuWinrateMU");
			}
		}

		public double SramWinrateMU
		{
			get { return _sramWinrateMu; }
			set
			{
				_sramWinrateMu = value;
				OnPropertyChanged("SramWinrateMU");
			}
		}

		public double XelWinrateMU
		{
			get { return _xelWinrateMu; }
			set
			{
				_xelWinrateMu = value;
				OnPropertyChanged("XelWinrateMU");
			}
		}

		public double SacriWinrateMU
		{
			get { return _sacriWinrateMu; }
			set { _sacriWinrateMu = value;
				OnPropertyChanged("SacriWinrateMU");
			}
		}

		public double SadiWinrateMU
		{
			get { return _sadiWinrateMu; }
			set { _sadiWinrateMu = value;
				OnPropertyChanged("SadiWinrateMU");
			}
		}

		public List<Match> GetAllMatchsFromGameType(GameType gameType)
		{
			var allKrosClass = TrackerSrv.TrackerModel.FilteredGames;
			return allKrosClass.FindAll(x => x.MatchType == gameType);
		}
		public List<Match> GetAllRankedMatchs()
		{
			return GetAllMatchsFromGameType(GameType.RANDOM_RANKED);
		}

		public List<Match> GetAllRankedMatchsByClass(ClassEnum classSelected)
		{
			var allRankedMatch = GetAllMatchsFromGameType(GameType.RANDOM_RANKED);
			var allRankedMatchFilteredPerClass = allRankedMatch.FindAll(match =>
				match.PlayerClassName == classSelected.ToString());
			return allRankedMatchFilteredPerClass;

		}

		public void CalculAllMatchup(ClassEnum classSelected)
		{
			var AllMatches = GetAllRankedMatchsByClass(classSelected);
			var allCharts = AllMatches.GroupBy(x => x.PlayerClasse)

				.OrderBy(x => x.Key)
				.Select(x =>
					new ChartStats
					{
						Name = x.Key,
						Value = Math.Round(100.0 * x.Count(g => g.ResultatMatch == (int)GameResult.Win) / x.Count(), 1),
						Brush = new SolidColorBrush(Helpers.Helpers.GetClassColor(x.Key, true)),
						Class = StatsCore.GetKrosClassByName(x.Key)
					});
			foreach (ChartStats stats in allCharts)
			{
				if (ClassEnum.TryParse(stats.Class.NameClass, out ClassEnum enumClassOut))
				{
					switch (enumClassOut)
					{
						case ClassEnum.None:
							
							break;
						case ClassEnum.Iop:
							IopWinrateMU = stats.Value;
							break;
						case ClassEnum.Cra:
							CraWinrateMU = stats.Value;
							break;
						case ClassEnum.Eniripsa:
							EniWinrateMU = stats.Value;
							break;
						case ClassEnum.Ecaflip:
							EcaWinrateMU = stats.Value;
							break;
						case ClassEnum.Enutrof:
							EnuWinrateMU = stats.Value;
							break;
						case ClassEnum.Sram:
							SramWinrateMU = stats.Value;
							break;
						case ClassEnum.Xelor:
							XelWinrateMU = stats.Value;
							break;
						case ClassEnum.Sacrieur:
							SacriWinrateMU = stats.Value;
							break;
						case ClassEnum.Sadida:
							SadiWinrateMU = stats.Value;
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(classSelected), classSelected, null);
					}
				}

			}

		}
	}
}
