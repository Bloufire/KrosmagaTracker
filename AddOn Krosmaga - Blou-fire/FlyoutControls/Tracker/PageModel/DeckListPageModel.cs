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
			get
			{
				if (_cardList != null) return _cardList.OrderByDescending(x => x.Card.GodType).ThenBy(x => x.Card.CostAP).ToList();
				_cardList = new List<DeckUI>();
				return _cardList;



			}
			set {
				_cardList = value;
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

		private bool _isShowPerTurn;

		public bool IsShowPerTurn
		{
			get { return _isShowPerTurn; }
			set
			{
				_isShowPerTurn = value;
				OnPropertyChanged("IsShowPerTurn");
			}
		}


		public DeckListPageModel()
		{
			
		}
	}
}
