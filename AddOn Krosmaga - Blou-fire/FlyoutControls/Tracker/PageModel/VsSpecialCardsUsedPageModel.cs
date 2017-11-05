using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.UIElements;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
    public class VsSpecialCardsUsedPageModel : ObservableObject
    {
        private List<DeckUI> _vsDeckInfinites;
        private List<DeckUI> _vsDeckKrosmiques;



        #region CTOR
        public VsSpecialCardsUsedPageModel()
        {

            TrackerSrv.TrackerModel.PropertyChanged += TrackerModel_PropertyChanged; ; ;


        }

        private void TrackerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            VsDeckInfinites = TrackerSrv.TrackerModel.DeckInfinites;
            VsDeckKrosmiques = TrackerSrv.TrackerModel.DeckKrosmiques;
        }

        #endregion

        public List<DeckUI> VsDeckInfinites
        {
            get
            {
                if (_vsDeckInfinites != null) return _vsDeckInfinites.OrderByDescending(x => x.Card.GodType).ThenBy(x => x.Card.CostAP).ToList();
                _vsDeckInfinites = new List<DeckUI>();
                return _vsDeckInfinites;

            }

            set
            {
                _vsDeckInfinites = value;
                OnPropertyChanged("VsDeckInfinites");
            }
        }

        public List<DeckUI> VsDeckKrosmiques
        {
            get
            {
                if (_vsDeckKrosmiques != null) return _vsDeckKrosmiques.OrderByDescending(x => x.Card.GodType).ThenBy(x => x.Card.CostAP).ToList();
                _vsDeckKrosmiques = new List<DeckUI>();
                return _vsDeckKrosmiques;

            }

            set
            {
                _vsDeckKrosmiques = value;
                OnPropertyChanged("VsDeckKrosmiques");
            }
        }
    }
}
