using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using AddOn_Krosmaga___Blou_fire.DataModel;
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

		private double _iopWinrateWin;
		private double _craWinrateWin;
		private double _eniWinrateWin;
		private double _ecaWinrateWin;
		private double _enuWinrateWin;
		private double _sramWinrateWin;
		private double _xelWinrateWin;
		private double _sacriWinrateWin;
		private double _sadiWinrateWin;

		private double _iopWinrateLose;
		private double _craWinrateLose;
		private double _eniWinrateLose;
		private double _ecaWinrateLose;
		private double _enuWinrateLose;
		private double _sramWinrateLose;
		private double _xelWinrateLose;
		private double _sacriWinrateLose;
		private double _sadiWinrateLose;
		private ClassEnum myClassPick;

		public List<KrosClass> ComboClasseValues { get; set; }
		public MatchUpData()
		{
			App myApplication = ((App)Application.Current);
			TrackerSrv = myApplication.TrackerCoreService;
			ComboClasseValues = StatsCore.GetAllClassAndImage();
			TrackerSrv.CurrentFiltersStatModel.PropertyChanged += CurrentFiltersStatModelFromEmptyClass_PropertyChanged;
			CalculAllMatchup(TrackerSrv.CurrentFiltersStatModel.SelectedClass);
		}

		public MatchUpData(ClassEnum myClassPick)
		{
			this.myClassPick = myClassPick;
			App myApplication = ((App)Application.Current);
			TrackerSrv = myApplication.TrackerCoreService;
			ComboClasseValues = StatsCore.GetAllClassAndImage();
			TrackerSrv.CurrentFiltersStatModel.PropertyChanged += CurrentFiltersStatModel_PropertyChanged;
			if (myClassPick != ClassEnum.None)
				KrosClassUsed = ComboClasseValues.First(x => x.NameClass == myClassPick.ToString());
			CalculAllMatchup(myClassPick);
		}

		private void CurrentFiltersStatModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ClassEnum.TryParse(KrosClassUsed.NameClass, out ClassEnum enumClassOut);
			CalculAllMatchup(enumClassOut);
		}
		private void CurrentFiltersStatModelFromEmptyClass_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			
			CalculAllMatchup(TrackerSrv.CurrentFiltersStatModel.SelectedClass);
		}
		#region properties
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
			set
			{
				_eniWinrateMu = value; OnPropertyChanged("EniWinrateMU");
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
			set
			{
				_sacriWinrateMu = value;
				OnPropertyChanged("SacriWinrateMU");
			}
		}

		public double SadiWinrateMU
		{
			get { return _sadiWinrateMu; }
			set
			{
				_sadiWinrateMu = value;
				OnPropertyChanged("SadiWinrateMU");
			}
		}
		#endregion

		#region Win-Lose
		
		public double IopWinrateWin
		{
			get { return _iopWinrateWin; }
			set
			{
				_iopWinrateWin = value;
				OnPropertyChanged("IopWinrateWin");
				OnPropertyChanged("IopWinrateDisplay");
			}
		}
		public double CraWinrateWin
		{
			get { return _craWinrateWin; }
			set
			{
				_craWinrateWin = value;
				OnPropertyChanged("CraWinrateWin");
				OnPropertyChanged("CraWinrateDisplay");
			}
		}

		public double EniWinrateWin
		{
			get { return _eniWinrateWin; }
			set
			{
				_eniWinrateWin = value; OnPropertyChanged("EniWinrateWin");
				OnPropertyChanged("EniWinrateDisplay");
			}
		}

		public double EcaWinrateWin
		{
			get { return _ecaWinrateWin; }
			set
			{
				_ecaWinrateWin = value;
				OnPropertyChanged("EcaWinrateWin");
				OnPropertyChanged("EcaWinrateDisplay");
			}
		}

		public double EnuWinrateWin
		{
			get { return _enuWinrateWin; }
			set
			{
				_enuWinrateWin = value;
				OnPropertyChanged("EnuWinrateWin");
				OnPropertyChanged("EnuWinrateDisplay");
			}
		}

		public double SramWinrateWin
		{
			get { return _sramWinrateWin; }
			set
			{
				_sramWinrateWin = value;
				OnPropertyChanged("SramWinrateWin");
				OnPropertyChanged("SramWinrateDisplay");
			}
		}

		public double XelWinrateWin
		{
			get { return _xelWinrateWin; }
			set
			{
				_xelWinrateWin = value;
				OnPropertyChanged("XelWinrateWin");
				OnPropertyChanged("XelWinrateDisplay");
			}
		}

		public double SacriWinrateWin
		{
			get { return _sacriWinrateWin; }
			set
			{
				_sacriWinrateWin = value;
				OnPropertyChanged("SacriWinrateWin");
				OnPropertyChanged("SacriWinrateDisplay");
			}
		}

		public double SadiWinrateWin
		{
			get { return _sadiWinrateWin; }
			set
			{
				_sadiWinrateWin = value;
				OnPropertyChanged("SadiWinrateWin");
				OnPropertyChanged("SadiWinrateDisplay");
			}
		}

		public double IopWinrateLose
		{
			get { return _iopWinrateLose; }
			set
			{
				_iopWinrateLose = value;
				OnPropertyChanged("IopWinrateLose");
				OnPropertyChanged("IopWinrateDisplay");
			}
		}
		public double CraWinrateLose
		{
			get { return _craWinrateLose; }
			set
			{
				_craWinrateLose = value;
				OnPropertyChanged("CraWinrateLose");
				OnPropertyChanged("CraWinrateDisplay");
			}
		}

		public double EniWinrateLose
		{
			get { return _eniWinrateLose; }
			set
			{
				_eniWinrateLose = value; OnPropertyChanged("EniWinrateLose");
				OnPropertyChanged("EniWinrateDisplay");
			}
		}

		public double EcaWinrateLose
		{
			get { return _ecaWinrateLose; }
			set
			{
				_ecaWinrateLose = value;
				OnPropertyChanged("EcaWinrateLose");
				OnPropertyChanged("EniWinrateDisplay");
			}
		}

		public double EnuWinrateLose
		{
			get { return _enuWinrateLose; }
			set
			{
				_enuWinrateLose = value;
				OnPropertyChanged("EnuWinrateLose");
				OnPropertyChanged("EnuWinrateDisplay");
			}
		}

		public double SramWinrateLose
		{
			get { return _sramWinrateLose; }
			set
			{
				_sramWinrateLose = value;
				OnPropertyChanged("SramWinrateLose");
				OnPropertyChanged("SramWinrateDisplay");
			}
		}

		public double XelWinrateLose
		{
			get { return _xelWinrateLose; }
			set
			{
				_xelWinrateLose = value;
				OnPropertyChanged("XelWinrateLose");
				OnPropertyChanged("XelWinrateDisplay");
			}
		}

		public double SacriWinrateLose
		{
			get { return _sacriWinrateLose; }
			set
			{
				_sacriWinrateLose = value;
				OnPropertyChanged("SacriWinrateLose");
				OnPropertyChanged("SacriWinrateDisplay");
			}
		}

		public double SadiWinrateLose
		{
			get { return _sadiWinrateLose; }
			set
			{
				_sadiWinrateLose = value;
				OnPropertyChanged("SadiWinrateLose");
				OnPropertyChanged("SadiWinrateDisplay");
			}
		}
		#endregion

		#region Display W-L

		public string IopWinrateDisplay
		{
			get { return $"{IopWinrateWin}-{IopWinrateLose}"; }
			
		}
		public string CraWinrateDisplay
		{
			get { return $"{CraWinrateWin}-{CraWinrateLose}"; }
			
		}

		public string EniWinrateDisplay
		{
			get { return $"{EniWinrateWin}-{EniWinrateLose}"; }
			
		}

		public string EcaWinrateDisplay
		{
			get { return $"{EcaWinrateWin}-{EcaWinrateLose}"; }
			
		}

		public string EnuWinrateDisplay
		{
			get { return $"{EnuWinrateWin}-{EnuWinrateLose}"; }
			
		}

		public string SramWinrateDisplay
		{
			get { return $"{SramWinrateWin}-{SramWinrateLose}"; }
		
		}

		public string XelWinrateDisplay
		{
			get { return $"{XelWinrateWin}-{XelWinrateLose}"; }
		
		}

		public string SacriWinrateDisplay
		{
			get { return $"{SacriWinrateWin}-{SacriWinrateLose}"; }
		
		}

		public string SadiWinrateDisplay
		{
			get { return $"{SadiWinrateWin}-{SadiWinrateLose}"; }
		}
		#endregion

		public List<Match> GetAllMatchsFromGameType(GameType gameType)
		{
			var allKrosClass = TrackerSrv.TrackerModel.FilteredGames;
			return allKrosClass.FindAll(x => x.MatchType == gameType);
		}
		public List<Match> GetAllRankedMatchs(GameType gameType)
		{
			return GetAllMatchsFromGameType(gameType);
		}

		public List<Match> GetAllRankedMatchsByClass(ClassEnum classSelected,GameType gameType)
		{
			var allRankedMatch = GetAllMatchsFromGameType(gameType);
			var allRankedMatchFilteredPerClass = allRankedMatch.FindAll(match =>
				match.PlayerClassName == classSelected.ToString());
			return allRankedMatchFilteredPerClass;

		}

		public KrosClass KrosClassUsed { get; set; }



		public void CalculAllMatchup(ClassEnum classSelected)
		{
			ResetAllMatchUp();
	
			var AllMatches = GetAllRankedMatchsByClass(classSelected,TrackerSrv.CurrentFiltersStatModel.SelectedGameType);
			var allCharts = AllMatches.GroupBy(x => x.OppenentClassName)

				.OrderBy(x => x.Key)
				.Select(x =>
					new ChartStats
					{
						Name = x.Key,
						Value = Math.Round(100.0 * x.Count(g => g.ResultatMatch == (int)GameResult.Win) / x.Count(), 1),
						NbWin = x.Count(g => g.ResultatMatch == (int)GameResult.Win),
						NbLose = x.Count(g => g.ResultatMatch == (int)GameResult.Loss),
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
							IopWinrateWin = stats.NbWin;
							IopWinrateLose = stats.NbLose;
							break;
						case ClassEnum.Cra:
							CraWinrateMU = stats.Value;
							CraWinrateWin = stats.NbWin;
							CraWinrateLose = stats.NbLose;
							break;
						case ClassEnum.Eniripsa:
							EniWinrateMU = stats.Value;
							EniWinrateWin = stats.NbWin;
							EniWinrateLose = stats.NbLose;
							break;
						case ClassEnum.Ecaflip:
							EcaWinrateMU = stats.Value;
							EcaWinrateWin = stats.NbWin;
							EcaWinrateLose = stats.NbLose;
							break;
						case ClassEnum.Enutrof:
							EnuWinrateMU = stats.Value;
							EnuWinrateWin = stats.NbWin;
							EnuWinrateLose = stats.NbLose;
							break;
						case ClassEnum.Sram:
							SramWinrateMU = stats.Value;
							SramWinrateWin = stats.NbWin;
							SramWinrateLose = stats.NbLose;
							break;
						case ClassEnum.Xelor:
							XelWinrateMU = stats.Value;
							XelWinrateWin = stats.NbWin;
							XelWinrateLose = stats.NbLose;
							break;
						case ClassEnum.Sacrieur:
							SacriWinrateMU = stats.Value;
							SacriWinrateWin = stats.NbWin;
							SacriWinrateLose = stats.NbLose;
							break;
						case ClassEnum.Sadida:
							SadiWinrateMU = stats.Value;
							SadiWinrateWin = stats.NbWin;
							SadiWinrateLose = stats.NbLose;
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(classSelected), classSelected, null);
					}
				}

			}
							
		}

		private void ResetAllMatchUp()
		{
			EcaWinrateMU = 0;
			IopWinrateMU = 0;
			CraWinrateMU = 0;
			EniWinrateMU = 0;
			EnuWinrateMU = 0;
			SramWinrateMU = 0;
			SacriWinrateMU = 0;
			SadiWinrateMU = 0;
			XelWinrateMU = 0;
			EcaWinrateWin = 0;
			IopWinrateWin = 0;
			CraWinrateWin = 0;
			EniWinrateWin = 0;
			EnuWinrateWin = 0;
			SramWinrateWin = 0;
			SacriWinrateWin = 0;
			SadiWinrateWin = 0;
			XelWinrateWin = 0;
			EcaWinrateLose = 0;
			IopWinrateLose = 0;
			CraWinrateLose = 0;
			EniWinrateLose = 0;
			EnuWinrateLose = 0;
			SramWinrateLose = 0;
			SacriWinrateLose = 0;
			SadiWinrateLose = 0;
			XelWinrateLose = 0;
		}
	}
}
