using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AddOn_Krosmaga___Blou_fire.Enums;

namespace AddOn_Krosmaga___Blou_fire.Converter
{
    public class SelectedToVisibilityConverter : IValueConverter
    {
	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    if (parameter == null) return Visibility.Hidden;
		    var nomDudMenu = (OverlayContentEnum)Enum.Parse(typeof(OverlayContentEnum), parameter.ToString());

		    if (value != null && nomDudMenu == (OverlayContentEnum)value)
		    {
			    return Visibility.Visible;

		    }
		    return Visibility.Hidden;
		}

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    throw new NotImplementedException();
	    }
    }
}
