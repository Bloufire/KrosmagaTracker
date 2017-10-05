﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AddOn_Krosmaga___Blou_fire.Converter
{
	public class CombiningConverter : IValueConverter
	{
		public IValueConverter Converter1 { get; set; }
		public IValueConverter Converter2 { get; set; }

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			object convertedValue = Converter1.Convert(value, targetType, parameter, culture);
			return Converter2.Convert(convertedValue, targetType, parameter, culture);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
