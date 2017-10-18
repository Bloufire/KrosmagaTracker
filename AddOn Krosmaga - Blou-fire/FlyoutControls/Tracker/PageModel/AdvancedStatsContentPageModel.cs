using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Data;
using AddOn_Krosmaga___Blou_fire.DataModel;
using AddOn_Krosmaga___Blou_fire.Enums;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class AdvancedStatsContentPageModel : ObservableObject
	{
		public List<KrosClass> ComboClasseValues { get; set; }
		public AdvancedStatsContentPageModel()
		{

			ComboClasseValues = StatsCore.GetAllClassAndImage();
			Matchups = new List<MatchUpData>();
			foreach (var classe in ComboClasseValues)
			{
				ClassEnum.TryParse(classe.NameClass, out ClassEnum myClassPick);
				var newMatchup = new MatchUpData();
				newMatchup.CalculAllMatchup(myClassPick);
				_matchups.Add(newMatchup);
			}

		}
		private List<MatchUpData> _matchups;

		public List<MatchUpData> Matchups
		{
			get { return _matchups; }
			set { _matchups = value; }
		}

	}


}
