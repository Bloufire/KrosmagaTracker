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
			switch ((string)value)
			{
				case "Iop":
					return "/Images/bg_gods/banner_iop.png";
					break;
				case "Ecaflip":
					return "/Images/bg_gods/banner_eca.png";
					break;
				case "Cra":
					return "/Images/bg_gods/banner_cra.png";
					break;
				case "Eniripsa":
					return "/Images/bg_gods/banner_eni.png";
					break;
				case "Sacrieur":
					return "/Images/bg_gods/banner_sacri.png";
					break;
				case "Sadida":
					return "/Images/bg_gods/banner_sadi.png";
					break;
				case "Sram":
					return "/Images/bg_gods/banner_sram.png";
					break;
				case "Xelor":
					return "/Images/bg_gods/banner_xel.png";
					break;
				case "Enutrof":
					return "/Images/bg_gods/banner_enu.png";
					break;
			}
			return "";

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}