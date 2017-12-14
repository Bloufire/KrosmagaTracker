using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AddOn_Krosmaga___Blou_fire.Converter
{
	public class ClassStringToClassBannerConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch ((string)value)
			{
				case "Iop":
					return "/Images/bg_gods/banner_iop.png";
				case "Ecaflip":				  
					return "/Images/bg_gods/banner_eca.png";
				case "Cra":					 
					return "/Images/bg_gods/banner_cra.png";
				case "Eniripsa":			 
					return "/Images/bg_gods/banner_eni.png";
				case "Sacrieur":			  
					return "/Images/bg_gods/banner_sacri.png";
				case "Sadida":				   
					return "/Images/bg_gods/banner_sadi.png";
				case "Sram":				
					return "/Images/bg_gods/banner_sram.png";
				case "Xelor":				
					return "/Images/bg_gods/banner_xel.png";
                case "Enutrof":
                    return "/Images/bg_gods/banner_enu.png";
                case "Feca":
                    return "/Images/bg_gods/banner_feca.png";
            }
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
