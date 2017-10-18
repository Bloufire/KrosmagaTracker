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
	}
}
