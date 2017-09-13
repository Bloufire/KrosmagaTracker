using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Utility;

namespace AddOn_Krosmaga___Blou_fire.Enums
{
	public class LocDescriptionAttribute : Attribute
	{
		public string LocDescription { get; }

		public LocDescriptionAttribute(string key, bool upper = false)
		{
			LocDescription = LocUtil.Get(key, upper)?.Replace("\\n", Environment.NewLine);
		}
	}
}