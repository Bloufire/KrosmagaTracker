using System.Collections.Generic;
using System.Linq;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.UIElements;
using NLog;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class VsCardsInHandPageModel : ObservableObject
    {
        private static Logger logger = LogManager.GetCurrentClassLogger(); // Logs
        public VsCardsInHandPageModel()
		{
			TrackerSrv.TrackerModel.PropertyChanged += TrackerModel_PropertyChanged;
		}
		private void TrackerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CardsInHand")
                VsCardsInHand = TrackerSrv.TrackerModel.CardsInHand;
		}
		private List<DeckUI> _vsCardsInHand;
		public List<DeckUI> VsCardsInHand
		{
			get
			{
				if (_vsCardsInHand != null) return _vsCardsInHand.OrderBy(x => x.DrawTurn).ToList();
				_vsCardsInHand = new List<DeckUI>();
				return _vsCardsInHand;
			}
			set
			{
				_vsCardsInHand = value;
                OnPropertyChanged("VsCardsInHand");
            }
        }
	}
}
