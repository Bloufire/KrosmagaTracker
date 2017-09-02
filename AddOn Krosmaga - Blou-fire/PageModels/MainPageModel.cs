using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Pages;
using MahApps.Metro.Controls;

namespace AddOn_Krosmaga___Blou_fire.PageModels
{
    public class MainPageModel : ObservableObject
    {
        public MainPageModel()
        {
	        BtnDiscord_OnClick = new RelayCommand(BtnDiscordOnClick);
	        // on associe la commande ChargerContactCommand à la méthode ChargeContact
            ChargerContactCommand = new RelayCommand(ChargeContact);
            BtnSettings_OnClick = new RelayCommand(BtnSettingsOnClick);
        }

        public string Stringtest => "test";
        public string FullName => _firstName + " " + _lastName;

        #region Properties

        private string _firstName = "default first";
        private string _lastName = "default last";

        public string FirstName
        {
            get => _firstName;

            set
            {
                if (_firstName == value) _firstName = "default first";
                _firstName = value;
                OnPropertyChanged("FirstName"); OnPropertyChanged("FullName");
            }
        }

        public string LastName
        {
            get => _lastName;

            set
            {
                if (_lastName == value) _lastName = "default last";
                _lastName = value;
                OnPropertyChanged("LastName"); OnPropertyChanged("FullName");
            }
        }

        #endregion


        #region Exemple appel cmd depuis Page 

        /// <summary>
        ///     Définition de la commande permettant de charger le contact
        /// </summary>
        public ICommand ChargerContactCommand { get; }

        /// <summary>
        ///     Méthode pour charger un contact depuis le model (service contacts)
        ///     On accède au service de façon très simple par un new, pas d'IOC ici...
        /// </summary>
        private void ChargeContact()
        {
            FirstName = "First";
            LastName = "Last";
        }

		#endregion

		#region Commands
		private MetroWindow settingsWindow;
		public ICommand BtnSettings_OnClick { get; }
        private void BtnSettingsOnClick()
        {
			if (settingsWindow != null)
	        {
		        settingsWindow.Activate();
		        return;
	        }

	        settingsWindow = new SettingsPage();
	        settingsWindow.Closed += (o, args) => settingsWindow = null;
	        settingsWindow.Show();
		}
    
        public ICommand BtnDiscord_OnClick { get; }
        private void BtnDiscordOnClick()
        {
	        Helper.TryOpenUrl("https://discord.gg/UR2TQEp"); 
        }

        #endregion
    }
}