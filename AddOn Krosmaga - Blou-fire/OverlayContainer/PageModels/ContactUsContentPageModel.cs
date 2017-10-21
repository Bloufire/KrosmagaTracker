using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;

namespace AddOn_Krosmaga___Blou_fire.OverlayContainer.PageModels
{
    public class ContactUsContentPageModel : ObservableObject
    {
	    public ContactUsContentPageModel()
	    {
		    

	    }

	    public ICommand BtnDiscord_OnClick => new RelayCommand(BtnDiscordOnClick);

		private void BtnDiscordOnClick()
	    {
		    Helpers.Helpers.TryOpenUrl("https://discord.gg/UR2TQEp");
	    }

	    public ICommand BtnTwitterKrosTracker_OnClick => new RelayCommand(BtnTwitterKrosTrackerOnClick);

	    private void BtnTwitterKrosTrackerOnClick()
	    {
		    Helpers.Helpers.TryOpenUrl("https://twitter.com/KrosmagaTracker");
	    }

	    public ICommand BtnTwitterBlou_OnClick => new RelayCommand(BtnTwitterBlouOnClick);

	    private void BtnTwitterBlouOnClick()
	    {
		    Helpers.Helpers.TryOpenUrl("https://twitter.com/BlouFire");
	    }

	    public ICommand BtnTwitterXeno_OnClick => new RelayCommand(BtnTwitterXenoOnClick);

	    private void BtnTwitterXenoOnClick()
	    {
		    Helpers.Helpers.TryOpenUrl("https://twitter.com/Xeno_CCG");
	    }

	    public ICommand BtnTwitterKindry_OnClick => new RelayCommand(BtnTwitterKindryOnClick);

	    private void BtnTwitterKindryOnClick()
	    {
		    Helpers.Helpers.TryOpenUrl("https://twitter.com/Kindryad");
	    }

	    public ICommand BtnTwitterGuiver_OnClick => new RelayCommand(BtnTwitterGuiverOnClick);

	    private void BtnTwitterGuiverOnClick()
	    {
		    Helpers.Helpers.TryOpenUrl("https://twitter.com/FR_Guiver");
	    }

	}
}
