using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Helpers;
using JsonCardsParser;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class CardPageModel : ObservableObject
	{
		private Card _cardToShow;

		public CardPageModel()
		{
			
		}

		public CardPageModel(Card cardData)
		{
			CardToShow = cardData;
		}

		public Card CardToShow
		{
			get { return _cardToShow; }
			set
			{
				_cardToShow = value;
				OnPropertyChanged(nameof(CardToShow));
			}
		}
	}
}
