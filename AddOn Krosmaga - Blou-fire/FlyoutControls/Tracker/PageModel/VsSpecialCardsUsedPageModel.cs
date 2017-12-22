using System.Collections.Generic;
using System.Linq;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.UIElements;
using NLog;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
    public class VsSpecialCardsUsedPageModel : ObservableObject
    {
        private static Logger logger = LogManager.GetCurrentClassLogger(); // Logs
        private List<DeckUI> _vsDeckInfinites;
        private List<DeckUI> _vsDeckKrosmiques;
        #region CTOR
        public VsSpecialCardsUsedPageModel()
        {
            TrackerSrv.TrackerModel.PropertyChanged += TrackerModel_PropertyChanged;
        }

        private void TrackerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DeckInfinites" || e.PropertyName == "Deck")
                VsDeckInfinites = TrackerSrv.TrackerModel.DeckInfinites;
            if (e.PropertyName == "DeckKrosmiques" || e.PropertyName == "Deck")
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
