using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class VsProfileDataPageModel : ObservableObject
	{
		private int _vsLosesNb;
		private int _vsWinsNb;
		private int _vsNbFleau;
        private int _vsNbCardsInOwnHand;
        private int _vsNbCardsInHand;
        private string _vsPseudo;
		private string _vsCurrentTurn;
		private int _vsRank;
		private string _isRankVisible;
		private string _vsClass;
		#region CTOR
		public VsProfileDataPageModel()
		{
			TrackerSrv.TrackerModel.PropertyChanged += TrackerModel_PropertyChanged;
		}
		private void UpdateScreen(string PropertyName)
		{
            if (PropertyName == "VsPseudo")
			    VsPseudo = TrackerSrv.TrackerModel.VsPseudo;
            if (PropertyName == "VsWinsNb")
                VsWinsNb = TrackerSrv.TrackerModel.VsWinsNb;
            if (PropertyName == "NbFleau")
                VsNbFleau = TrackerSrv.TrackerModel.NbFleau;
            if (PropertyName == "VsLosesNb")
                VsLosesNb = TrackerSrv.TrackerModel.VsLosesNb;
            if (PropertyName == "OpponentCardsInHand")
                VsNbCardsInHand = TrackerSrv.TrackerModel.OpponentCardsInHand;
            if (PropertyName == "OwnCardsInHand")
                VsNbCardsInOwnHand = TrackerSrv.TrackerModel.OwnCardsInHand;
            if (PropertyName == "OpponentLevel")
                VsRank = TrackerSrv.TrackerModel.OpponentLevel;
            if (PropertyName == "CurrentTurn")
                VsCurrentTurn = "Turn " + TrackerSrv.TrackerModel.CurrentTurn.ToString();
            if (PropertyName == "OpponentClasse")
                VsClass = TrackerSrv.TrackerModel.OpponentClasse;
		}
		private void TrackerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
            if (e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.VsPseudo))
                || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.VsWinsNb))
               || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.NbFleau))
               || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.VsLosesNb))
               || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.OwnCardsInHand))
               || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.OpponentCardsInHand))
               || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.OpponentLevel))
               || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.OpponentClasse))
               || e.PropertyName.Equals(nameof(TrackerSrv.TrackerModel.CurrentTurn)))
            {
                UpdateScreen(e.PropertyName);
            }
		}
		#endregion
		#region Properties
		public int VsLosesNb
		{
			get { return _vsLosesNb; }
			set
			{
				_vsLosesNb = value;
				OnPropertyChanged(nameof(VsLosesNb));
			}
		}
		public string IsRankVisible
		{
			get
            {
                if (_isRankVisible != "Visible")
                    _isRankVisible = "Hidden";
                return _isRankVisible;
            }
			set
			{
				_isRankVisible = value;
				OnPropertyChanged(nameof(IsRankVisible));
			}
		}
		public int VsRank
		{
			get
			{
				return _vsRank;
			}
			set
			{
				_vsRank = value;
                if (VsRank == 0)
                    IsRankVisible = "Hidden";
                else
                    IsRankVisible = "Visible";
                //IsRankVisible = VsRank != 0;
				OnPropertyChanged(nameof(VsRank));
			}
		}
		public int VsWinsNb
		{
			get { return _vsWinsNb; }
			set
			{
				_vsWinsNb = value;
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
        public int VsNbCardsInOwnHand
        {
            get { return _vsNbCardsInOwnHand; }
            set
            {
                _vsNbCardsInOwnHand = value;
                OnPropertyChanged(nameof(VsNbCardsInOwnHand));
            }
        }
        public string VsPseudo
		{
			get
			{
				return string.IsNullOrEmpty(_vsPseudo) ? "     " : _vsPseudo;
			}
			set
			{
				_vsPseudo = value;
				OnPropertyChanged(nameof(VsPseudo));
			}
		}
		public string VsCurrentTurn
		{
			get { return _vsCurrentTurn; }
			set
			{
				_vsCurrentTurn = value;
				OnPropertyChanged(nameof(VsCurrentTurn));
			}
		}
		public string VsClass
		{
			get { return _vsClass; }
			set
			{
				_vsClass = value;
				OnPropertyChanged(nameof(VsClass));
			}
		}
		#endregion
	}
}
