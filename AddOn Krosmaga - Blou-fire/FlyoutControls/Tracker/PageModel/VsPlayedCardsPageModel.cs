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
    public class VsPlayedCardsPageModel : ObservableObject
	{
		private List<DeckUI> _vsDeck;



		#region CTOR
		public VsPlayedCardsPageModel()
		{
		
			TrackerSrv.TrackerModel.PropertyChanged += TrackerModel_PropertyChanged; ; ;


		}

		private void TrackerModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			VsDeck = TrackerSrv.TrackerModel.Deck;
		}

		#endregion

		public List<DeckUI> VsDeck
		{
			get
			{
				if (_vsDeck != null) return _vsDeck.OrderByDescending(x => x.Card.GodType).ThenBy(x => x.Card.CostAP).ToList();
				_vsDeck = new List<DeckUI>();
				return _vsDeck;

			}

			set
			{
				_vsDeck = value;
				OnPropertyChanged("VsDeck");
			}
		}
	}
}
