using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.UIElements;
using JsonCardsParser;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

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
				SeriesCollection = GetSeriesCollectionFromDeck(CardList);
				OnPropertyChanged("CardList"); }
		}

	
		private SeriesCollection GetSeriesCollectionFromDeck(List<DeckUI> cardList)
		{
			var CreaValues  = GetDeckValues(cardList,0);
			var SpellValues = GetDeckValues(cardList,1);
			var returnSeries = new SeriesCollection
			{
				new StackedColumnSeries
				{
					//Liste des cartes Créatures par cout
					Values =CreaValues,// new ChartValues<double> {0, 2, 5, 2, 4, 5, 0, 0},
					StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
					Name = "Invocations",
					DataLabels = true
				},
				new StackedColumnSeries
				{
					//Liste des cartes Sorts par cout
					Values =  SpellValues, // new ChartValues<double> {1, 3, 1, 5, 0, 1, 1, 2},
					Name = "Spells",
					StackMode = StackMode.Values,
					DataLabels = true
				}
			};
			return returnSeries;
		}

		private ChartValues<double> GetDeckValues(List<DeckUI> cardList,int InvocOrSpell)
		{
			var values = new ChartValues<double>();
			for (var i = 0; i <= 6; i++)
			{
				var value = 0;
				foreach (var card in cardList.FindAll(x => x.Card.CostAP == i && x.Card.CardType == InvocOrSpell))
				{
					value += card.CardCount;
				}
				values.Add(value);
			}

			var valueMore = 0;
			foreach (var card in cardList.FindAll(x => x.Card.CostAP >= 7 && x.Card.CardType == InvocOrSpell))
			{
				valueMore += card.CardCount;
			}
			values.Add(valueMore);
			return values;
		}

		


		#region MyRegion
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
		#endregion

		#region Charts Data 


		public SeriesCollection SeriesCollection { get; set; }
		
		#endregion

		public DeckListPageModel()
		{
			Labels = new[] { "0", "1", "2", "3", "4", "5", "6", "7+" };
		}

		public string[] Labels { get; set; }
	}
}
