using AddOn_Krosmaga___Blou_fire.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Models
{
	/// <summary>
	/// Model contenant toutes les informations que le tracker/lecture en base peuvent remonter. Et qui seront utilisable pour les vues.
	/// </summary>
	public class TrackerModel : ObservableObject
	{

		#region Own Profile Data

		#region OwnPseudo
		private string _ownPseudo;
		public string OwnPseudo
		{
			get => _ownPseudo;

			set
			{
				if (_ownPseudo == value) _ownPseudo = "default first";
				_ownPseudo = value;
				OnPropertyChanged("OwnPseudo");
			}
		} 
		#endregion

		#region OwnWinsNb
		private int _ownWinsNb;
		public int OwnWinsNb
		{
			get => _ownWinsNb;

			set
			{
				if (_ownWinsNb == null)
				{
					_ownWinsNb = -1;
				}

				_ownWinsNb = value;
				OnPropertyChanged("OwnWinsNb");
			}
		}
		#endregion

		#region OwnLosesNb
		private int _ownLosesNb;
		public int OwnLosesNb
		{
			get => _ownLosesNb;

			set
			{
				if (_ownLosesNb == null)
				{
					_ownLosesNb = -1;
				}

				_ownLosesNb = value;
				OnPropertyChanged("OwnLosesNb");
			}
		}
		#endregion


		#endregion

		#region Vs Profile Data

		#region VsPseudo
		private string _vsPseudo;
		public string VsPseudo
		{
			get => _vsPseudo;

			set
			{
				if (_vsPseudo == value) _vsPseudo = "default first";
				_vsPseudo = value;
				OnPropertyChanged("VsPseudo");
			}
		}
		#endregion

		#region VsWinsNb
		private int _vsWinsNb;
		public int VsWinsNb
		{
			get => _vsWinsNb;

			set
			{
				if (_vsWinsNb == null)
				{
					_vsWinsNb = -1;
				}

				_vsWinsNb = value;
				OnPropertyChanged("VsWinsNb");
			}
		}
		#endregion

		#region VsLosesNb
		private int _vsLosesNb;
		public int VsLosesNb
		{
			get => _vsLosesNb;

			set
			{
				if (_vsLosesNb == null)
				{
					_vsLosesNb = -1;
				}

				_vsLosesNb = value;
				OnPropertyChanged("VsLosesNb");
			}
		}
		#endregion


		#endregion


	}
}
