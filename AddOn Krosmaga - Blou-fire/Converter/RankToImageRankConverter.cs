using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AddOn_Krosmaga___Blou_fire.Converter
{
	public class RankToImageRankConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return "";
			switch ((int) value)
			{
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					return "/Images/rank/rank_silver.png";

				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
					return "/Images/rank/rank_bronze.png";

				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
					return "/Images/rank/rank_gold.png";
				case 22:
				case 23:
				case 24:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:

					return "/Images/rank/rank_platine.png";

				case 30:

					return "/Images/rank/rank_veteran.png";
			}

			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}