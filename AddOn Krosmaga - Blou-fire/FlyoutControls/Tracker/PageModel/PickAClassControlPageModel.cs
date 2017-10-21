using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Data;
using AddOn_Krosmaga___Blou_fire.DataModel;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Models;
using AddOn_Krosmaga___Blou_fire.Services;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
    public class PickAClassControlPageModel : ObservableObject
    {
        
        public List<KrosClass> ComboClasseValues { get; set; }
		public List<GameType> ComboGameTypeValues { get; set; }

		public PickAClassControlPageModel()
        {
            ComboClasseValues = StatsCore.GetAllClassAndImage();
			if(ComboGameTypeValues == null)
				ComboGameTypeValues = new List<GameType>();

			ComboGameTypeValues.Add(GameType.RANDOM_RANKED);
			ComboGameTypeValues.Add(GameType.VERSUS_IA);
	        ComboGameTypeValues.Add(GameType.RANDOM_UNRANKED);
			TrackerSrv.CurrentFiltersStatModel = new FiltersStatModel();
        }

        #region ClasseSelection

        public ICommand ComboBoxClasseOnSelectionChanged => new RelayCommand(ComboBoxClasse_OnSelectionChangedAction);

        private KrosClass _selectedItemClasse;

        public KrosClass SelectedItemClasse
        {
            get => this._selectedItemClasse;
            set
            {
                this._selectedItemClasse = value;

                var v = ClassEnum.TryParse(_selectedItemClasse.NameClass, out ClassEnum result);
                TrackerSrv.CurrentFiltersStatModel.SelectedClass = result;
                OnPropertyChanged("SelectedItemClasse");
            }
        }

	    private GameType _selectedItemGameType;

	    public GameType SelectedItemGameType
		{
		    get => this._selectedItemGameType;
		    set
		    {
			    this._selectedItemGameType = value;

			    TrackerSrv.CurrentFiltersStatModel.SelectedGameType = _selectedItemGameType;
			    OnPropertyChanged("SelectedItemClasse");
		    }
	    }
		
		private void ComboBoxClasse_OnSelectionChangedAction()
        {
            if (_selectedItemClasse != null)
            {
                Debug.WriteLine("SelectedItemClasse = " + _selectedItemClasse);
            }
        }

        #endregion




    }
}
