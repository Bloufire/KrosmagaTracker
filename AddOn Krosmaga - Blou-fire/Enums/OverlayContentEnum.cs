using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Enums
{
	public enum OverlayContentEnum
	{
		[LocDescription("Defaut")]
		Default = 1,
		[LocDescription("Tracker")]
		Tracker,
		[LocDescription("Stats")]
		Stats,
		[LocDescription("Settings")]
		Settings,
		[LocDescription("Histo")]
		Histo
	}
}
