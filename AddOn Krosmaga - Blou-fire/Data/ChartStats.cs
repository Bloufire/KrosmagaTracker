using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using AddOn_Krosmaga___Blou_fire.DataModel;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	public class ChartStats
	{
		public string Name { get; set; }
		public KrosClass Class { get; set; }
		public double Value { get; set; }
		public Brush Brush { get; set; }
	}
}
