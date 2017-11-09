using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.UIElements;

namespace AddOn_Krosmaga___Blou_fire.OverlayContainer.PageModels
{
	public class DecksComparerPageModel : ObservableObject
	{
		#region Deck B
		private DeckListPageModel _deckBModel;

		public DeckListPageModel DeckBModel
		{
			get { return _deckBModel; }
			set
			{
				_deckBModel = value;
				OnPropertyChanged("DeckBModel");
			}
		}
		#endregion

		#region Deck A
		private DeckListPageModel _deckAModel;

		public DeckListPageModel DeckAModel
		{
			get { return _deckAModel; }
			set
			{
				_deckAModel = value;
				OnPropertyChanged("DeckAModel");
			}
		} 
		#endregion
	
	}
}
