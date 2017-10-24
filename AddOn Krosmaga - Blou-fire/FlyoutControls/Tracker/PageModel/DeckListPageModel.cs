using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.UIElements;
using JsonCardsParser;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class DeckListPageModel : ObservableObject
	{
		private List<DeckUI> _cardList;

		public List<DeckUI> CardList
		{
			get { return _cardList; }
			set { _cardList = value;
				OnPropertyChanged("CardList"); }
		}
		private string _headerName;

		public string HeaderName
		{
			get { return _headerName; }
			set
			{
				_headerName = value;
				OnPropertyChanged("HeaderName");
			}
		}

		public DeckListPageModel()
		{
			CardList = new List<DeckUI>();
		}
	}
}
