using System.Collections.ObjectModel;
using System.Windows.Input;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Pages;
using MahApps.Metro.Controls;

namespace AddOn_Krosmaga___Blou_fire.PageModels
{
	public class ClassWin : ObservableObject
	{

		private string _name = string.Empty;
		private int _count = 0;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged("Name");
			}
		}

		public int Count
		{
			get { return _count; }
			set
			{
				_count = value;
				OnPropertyChanged("Name");
			}

		}
	}


	public class MainPageModel : ObservableObject
	{
		public MainPageModel()
		{
			BtnDiscord_OnClick = new RelayCommand(BtnDiscordOnClick);
			// on associe la commande ChargerContactCommand à la méthode ChargeContact
			ChargerContactCommand = new RelayCommand(ChargeContact);
			BtnSettings_OnClick = new RelayCommand(BtnSettingsOnClick);



			_classeWins.Add(new ClassWin() { Name = Enums.ClassEnum.Cra.ToString(), Count = 1340 });
			_classeWins.Add(new ClassWin() { Name = Enums.ClassEnum.Eniripsa.ToString(), Count = 1220 });
			_classeWins.Add(new ClassWin() { Name = Enums.ClassEnum.Sacrieur.ToString(), Count = 309 });
			_classeWins.Add(new ClassWin() { Name = Enums.ClassEnum.Sram.ToString(), Count = 240 });
			_classeWins.Add(new ClassWin() { Name = Enums.ClassEnum.Xelor.ToString(), Count = 195 });
			_classeWins.Add(new ClassWin() { Name = Enums.ClassEnum.Sacrieur.ToString(), Count = 174 });
			_classeWins.Add(new ClassWin() { Name = Enums.ClassEnum.Ecaflip.ToString(), Count = 158 });
		}

		private readonly ObservableCollection<ClassWin> _classeWins = new ObservableCollection<ClassWin>();
		public ObservableCollection<ClassWin> ClasseWins
		{
			get { return _classeWins; }
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