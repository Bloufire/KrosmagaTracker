using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AddOn_Krosmaga___Blou_fire.DataModel;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Services;
using AddOn_Krosmaga___Blou_fire.UIElements;

namespace AddOn_Krosmaga___Blou_fire.Data
{
    public class StatsCore
    {
	    protected static TrackerCoreSrv TrackerSrv;

	    public StatsCore()
	    {
			App myApplication = ((App)Application.Current);
		    TrackerSrv = myApplication.TrackerCoreService;
		}
		/// <summary>
		/// Récupère une liste de tout les matchs
		/// </summary>
		/// <returns></returns>
		public static List<KrosClass> GetAllClassAndImage()
		    {
			    var ComboClasseValues = Enum.GetValues(typeof(ClassEnum));

			    var listToReturn = new List<KrosClass>();
			    listToReturn.Add(new KrosClass("Iop", "../../Images/Gods/Iop.png"));
			    listToReturn.Add(new KrosClass("Cra", "../../Images/Gods/Cra.png"));
			    listToReturn.Add(new KrosClass("Eniripsa", "../../Images/Gods/Eni.png"));
			    listToReturn.Add(new KrosClass("Enutrof", "../../Images/Gods/Enu.png"));
			    listToReturn.Add(new KrosClass("Sacrieur", "../../Images/Gods/Sacri.png"));
			    listToReturn.Add(new KrosClass("Sram", "../../Images/Gods/Sram.png"));
			    listToReturn.Add(new KrosClass("Xelor", "../../Images/Gods/Xel.png"));
			    listToReturn.Add(new KrosClass("Sadida", "../../Images/Gods/Sadi.png"));
			    listToReturn.Add(new KrosClass("Ecaflip", "../../Images/Gods/Eca.png"));
			    var orderedList = listToReturn.OrderBy(x => x.NameClass).ToList();
			    return orderedList;
		    }

	    public static List<Match> GetAllMatchsFromGameType(GameType gameType)
	    {
		    var allKrosClass = TrackerSrv.TrackerModel.FilteredGames;
		    return allKrosClass.FindAll(x => x.MatchType == gameType);
	    }
	    public static List<Match> GetAllMatchsFromGameTypeAndClass(GameType gameType,ClassEnum classSelected)
	    {
		    var allKrosClass = TrackerSrv.TrackerModel.FilteredGames;
		    return allKrosClass.FindAll(x => x.MatchType == gameType && x.PlayerClassName == classSelected.ToString());
	    }

		public static KrosClass GetKrosClassByName(string name)
		    {
			    var allKrosClass = GetAllClassAndImage();
			    return allKrosClass.Find(x => x.NameClass == name);

		    }
	    
	}
}
