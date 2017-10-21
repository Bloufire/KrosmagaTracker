using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel
{
	public class SettingsOverlayParamPageModel : ObservableObject
	{
	    public SettingsOverlayParamPageModel()
	    {
	        
	    }


        #region Properties

        private ICommand _closeOverlayCommand;
        public ICommand CloseOverlay
        {
            get
            {

                if (_closeOverlayCommand == null)
                    _closeOverlayCommand = new AsyncCommand(CloseOverlayNow);

                return _closeOverlayCommand;
            }
        }

	    private async Task CloseOverlayNow()
	    {

	        var metroWindow = (Application.Current.MainWindow as MetroWindow);
	        var res = await metroWindow.ShowMessageAsync("Confirmation", "Are your sure you want to exit ?",MessageDialogStyle.AffirmativeAndNegative,new MetroDialogSettings()
	        {
	            DialogTitleFontSize = 15,
                DialogMessageFontSize = 13,
				AffirmativeButtonText = "Yes",
				NegativeButtonText = "No",
                DefaultButtonFocus = MessageDialogResult.Affirmative
		     
			} 
            );
	         
	        if (res == MessageDialogResult.Affirmative)
                System.Windows.Application.Current.Shutdown();
        }

	    #endregion
    }
}
