using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public PickAClassControlPageModel()
        {
            ComboClasseValues = KrosClassData.GetAllClassAndImage();
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
