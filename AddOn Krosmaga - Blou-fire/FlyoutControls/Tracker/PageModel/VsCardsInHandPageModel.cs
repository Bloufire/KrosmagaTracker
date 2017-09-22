using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.UIElements;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class VsCardsInHandPageModel : ObservableObject
	{


		public VsCardsInHandPageModel()
		{
			TrackerSrv.TrackerModel.PropertyChanged += TrackerModel_PropertyChanged;
		}

		private void TrackerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			VsCardsInHand = TrackerSrv.TrackerModel.CardsInHand;
		}

		private List<DeckUI> _vsCardsInHand;
		public List<DeckUI> VsCardsInHand
		{
			get
			{
				if (_vsCardsInHand != null) return _vsCardsInHand.OrderByDescending(x => x.Card.GodType).ThenBy(x => x.Card.CostAP).ToList();
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
