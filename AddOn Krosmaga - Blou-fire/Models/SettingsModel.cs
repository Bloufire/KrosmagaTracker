using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Models
{
	public class SettingsModel
	{
	
			public int MaxThreads { get; set; }
			public string Endpoint { get; set; }
			public IEnumerable<string> BannedPhrases { get; set; }
		
	}
}
