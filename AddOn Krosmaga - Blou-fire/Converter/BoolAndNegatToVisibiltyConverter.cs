using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AddOn_Krosmaga___Blou_fire.Converter
{
    public class BoolAndNegatToVisibiltyConverter : IValueConverter
    {
	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    if (value != null)
		    {
			    if ((bool)value) return Visibility.Collapsed;
			    if (!(bool)value) return Visibility.Visible;
		    }
		    return null;
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    return null;
	    }
	}
}
